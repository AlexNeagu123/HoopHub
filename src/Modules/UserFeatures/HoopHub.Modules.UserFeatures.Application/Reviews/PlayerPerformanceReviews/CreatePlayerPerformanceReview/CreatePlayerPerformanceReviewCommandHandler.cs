using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.CreatePlayerPerformanceReview
{
    public class CreatePlayerPerformanceReviewCommandHandler(
        IPlayerPerformanceReviewRepository playerPerformanceReviewRepository,
        ICurrentUserService userService)
        : IRequestHandler<CreatePlayerPerformanceReviewCommand, Response<PlayerPerformanceReviewDto>>
    {
        private readonly IPlayerPerformanceReviewRepository _playerPerformanceReviewRepository = playerPerformanceReviewRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly PlayerPerformanceReviewMapper _playerPerformanceReviewMapper = new();

        public async Task<Response<PlayerPerformanceReviewDto>> Handle(CreatePlayerPerformanceReviewCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new CreatePlayerPerformanceReviewCommandValidator(_playerPerformanceReviewRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromFluentResult(validationResult);

            var playerPerformanceReviewResult = PlayerPerformanceReview.Create(request.HomeTeamId, request.VisitorTeamId, request.Date, fanId!, request.Rating, request.PlayerId);
            if (!playerPerformanceReviewResult.IsSuccess)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromKeyMessage(playerPerformanceReviewResult.ErrorMsg, ValidationKeys.PlayerPerformanceReview);

            var playerPerformanceReview = playerPerformanceReviewResult.Value;
            var averageRating = await _playerPerformanceReviewRepository.GetAverageRatingByPlayerId(playerPerformanceReview.PlayerId, request.Rating);
            playerPerformanceReview.UpdateAverage(averageRating);

            var addResult = await _playerPerformanceReviewRepository.AddAsync(playerPerformanceReview);
            if (!addResult.IsSuccess)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromKeyMessage(addResult.ErrorMsg, ValidationKeys.PlayerPerformanceReview);

            var addedPlayerPerformanceReview = await _playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(playerPerformanceReview.HomeTeamId, playerPerformanceReview.VisitorTeamId, playerPerformanceReview.PlayerId, playerPerformanceReview.Date, fanId!);

            return new Response<PlayerPerformanceReviewDto>
            {
                Success = true,
                Data = _playerPerformanceReviewMapper.PlayerPerformanceReviewToPlayerPerformanceReviewDto(addedPlayerPerformanceReview.Value, null)
            };
        }
    }
}

using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.UpdatePlayerPerformanceReview
{
    public class UpdatePlayerPerformanceReviewCommandHandler(
        IPlayerPerformanceReviewRepository playerPerformanceReviewRepository,
        ICurrentUserService userService)
        : IRequestHandler<UpdatePlayerPerformanceReviewCommand, Response<PlayerPerformanceReviewDto>>
    {
        private readonly IPlayerPerformanceReviewRepository _playerPerformanceReviewRepository = playerPerformanceReviewRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly PlayerPerformanceReviewMapper _playerPerformanceReviewMapper = new();

        public async Task<Response<PlayerPerformanceReviewDto>> Handle(UpdatePlayerPerformanceReviewCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new UpdatePlayerPerformanceReviewCommandValidator(_playerPerformanceReviewRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromFluentResult(validationResult);

            var playerPerformanceReviewResult = await _playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(request.HomeTeamId, request.VisitorTeamId, request.PlayerId, request.Date, fanId!);
            if (!playerPerformanceReviewResult.IsSuccess)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromKeyMessage(playerPerformanceReviewResult.ErrorMsg, ValidationKeys.PlayerPerformanceReview);

            var playerPerformanceReview = playerPerformanceReviewResult.Value;
            playerPerformanceReview.Update(request.Rating);

            var updatePlayerPerformanceReviewResult = await _playerPerformanceReviewRepository.UpdateAsync(playerPerformanceReview);
            if (!updatePlayerPerformanceReviewResult.IsSuccess)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromKeyMessage(updatePlayerPerformanceReviewResult.ErrorMsg, ValidationKeys.PlayerPerformanceReview);

            return new Response<PlayerPerformanceReviewDto>
            {
                Success = true,
                Data = _playerPerformanceReviewMapper.PlayerPerformanceReviewToPlayerPerformanceReviewDto(playerPerformanceReview)
            };
        }
    }
}

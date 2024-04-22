using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.GetPlayerPerformanceReview
{
    public class GetPlayerPerformanceReviewQueryHandler(
        IPlayerPerformanceReviewRepository playerPerformanceReviewRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetPlayerPerformanceReviewQuery, Response<PlayerPerformanceReviewDto>>
    {
        private readonly IPlayerPerformanceReviewRepository _playerPerformanceReviewRepository = playerPerformanceReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly PlayerPerformanceReviewMapper _playerPerformanceReviewMapper = new();

        public async Task<Response<PlayerPerformanceReviewDto>> Handle(GetPlayerPerformanceReviewQuery request, CancellationToken cancellationToken)
        {
            var fanId = _currentUserService.GetUserId;
            var validator = new GetPlayerPerformanceReviewQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<PlayerPerformanceReviewDto>.ErrorResponseFromFluentResult(validationResult);

            var playerPerformanceReviewResult = await _playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(request.HomeTeamId, request.VisitorTeamId, request.PlayerId, request.Date, fanId!);
            if (!playerPerformanceReviewResult.IsSuccess)
            {
                return new Response<PlayerPerformanceReviewDto>
                {
                    Success = true,
                    Data = new PlayerPerformanceReviewDto { HomeTeamId = request.HomeTeamId, VisitorTeamId = request.VisitorTeamId, Date = request.Date }
                };
            }

            var averageRating = await _playerPerformanceReviewRepository.GetAverageRatingByGameTupleId(request.HomeTeamId, request.VisitorTeamId, request.PlayerId, request.Date);
            return new Response<PlayerPerformanceReviewDto>
            {
                Success = true,
                Data = _playerPerformanceReviewMapper.PlayerPerformanceReviewToPlayerPerformanceReviewDto(playerPerformanceReviewResult.Value, averageRating)
            };
        }
    }
}

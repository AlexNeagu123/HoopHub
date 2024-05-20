using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.GetPlayerPerformanceReviewsByGame
{
    public class GetPlayerPerformanceReviewsByGameQueryHandler(
        IPlayerPerformanceReviewRepository playerPerformanceReviewRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetPlayerPerformanceReviewsByGameQuery, Response<IReadOnlyList<PlayerPerformanceAverageDto>>>
    {
        private readonly IPlayerPerformanceReviewRepository _playerPerformanceReviewRepository = playerPerformanceReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly PlayerPerformanceReviewMapper _playerPerformanceReviewMapper = new();

        public async Task<Response<IReadOnlyList<PlayerPerformanceAverageDto>>> Handle(GetPlayerPerformanceReviewsByGameQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetPlayerPerformanceReviewsByGameQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<PlayerPerformanceAverageDto>>.ErrorResponseFromFluentResult(validationResult);

            var performancesResult = await _playerPerformanceReviewRepository.GetAllAveragePerformancesByGameAsync(request.HomeTeamId, request.VisitorTeamId, request.Date);
            var performances = performancesResult.Value;

            List<PlayerPerformanceAverageDto> playerPerformancesAverageList = [];
            foreach (var performance in performances)
            {
                var averageRating = await _playerPerformanceReviewRepository.GetAverageRatingByGameTupleId(performance.HomeTeamId, performance.VisitorTeamId, performance.PlayerId, performance.Date);
                var ownRating = await _playerPerformanceReviewRepository.GetOwnRatingByTupleId(performance.HomeTeamId, performance.VisitorTeamId, performance.PlayerId, performance.Date, _currentUserService.GetUserId!);
                playerPerformancesAverageList.Add(_playerPerformanceReviewMapper.PlayerPerformanceReviewToPlayerPerformanceReviewAverageDto(performance, averageRating, ownRating));
            }

            return new Response<IReadOnlyList<PlayerPerformanceAverageDto>>
            {
                Success = true,
                Data = playerPerformancesAverageList
            };
        }
    }
}

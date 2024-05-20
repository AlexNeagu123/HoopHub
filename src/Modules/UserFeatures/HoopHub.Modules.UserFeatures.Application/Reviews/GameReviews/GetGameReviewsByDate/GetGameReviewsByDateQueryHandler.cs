using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReviewsByDate
{
    public class GetGameReviewsByDateQueryHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetGameReviewsByDateQuery, Response<IReadOnlyList<GameReviewAverageDto>>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly GameReviewMapper _gameReviewMapper = new();
        public async Task<Response<IReadOnlyList<GameReviewAverageDto>>> Handle(GetGameReviewsByDateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetGameReviewsByDateQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<GameReviewAverageDto>>.ErrorResponseFromFluentResult(validationResult);

            var reviewsResult = await _gameReviewRepository.FindByDateIncludingAll(request.Date);
            var reviews = reviewsResult.Value;

            List<GameReviewAverageDto> reviewsDtoList = [];
            foreach (var review in reviews)
            {
                var averageRating = await _gameReviewRepository.GetAverageRatingByGameTupleId(review.HomeTeamId, review.VisitorTeamId, request.Date);
                reviewsDtoList.Add(_gameReviewMapper.GameReviewToGameReviewAverageDto(review, averageRating));
            }

            return new Response<IReadOnlyList<GameReviewAverageDto>>
            {
                Success = true,
                Data = reviewsDtoList
            };
        }
    }
}

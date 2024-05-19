using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReviewsByDate
{
    public class GetGameReviewsByDateQueryHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetGameReviewsByDateQuery, Response<IReadOnlyList<GameReviewDto>>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly GameReviewMapper _gameReviewMapper = new();
        public async Task<Response<IReadOnlyList<GameReviewDto>>> Handle(GetGameReviewsByDateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetGameReviewsByDateQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<GameReviewDto>>.ErrorResponseFromFluentResult(validationResult);

            var fanId = _currentUserService.GetUserId!; 
            var reviewsResult = await _gameReviewRepository.FindByDateAndFanIdIncludingAll(request.Date, fanId);
            var reviews = reviewsResult.Value;

            List<GameReviewDto> reviewsDtoList = [];
            foreach (var review in reviews)
            {
                var averageRating = await _gameReviewRepository.GetAverageRatingByGameTupleId(review.HomeTeamId, review.VisitorTeamId, request.Date);
                reviewsDtoList.Add(_gameReviewMapper.GameReviewToGameReviewDto(review, averageRating));
            }

            return new Response<IReadOnlyList<GameReviewDto>>
            {
                Success = true,
                Data = reviewsDtoList
            };
        }
    }
}

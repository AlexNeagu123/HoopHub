using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetAllReviews
{
    public class GetAllReviewsPagedQueryHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetAllReviewsPagedQuery, PagedResponse<IReadOnlyList<GameReviewDto>>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly GameReviewMapper _gameReviewMapper = new();
        public async Task<PagedResponse<IReadOnlyList<GameReviewDto>>> Handle(GetAllReviewsPagedQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllReviewsPagedQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<IReadOnlyList<GameReviewDto>>.ErrorResponseFromFluentResult(validationResult);

            var reviews = await _gameReviewRepository.GetAllPagedAsync(request.Page, request.PageSize, request.HomeTeamId, request.VisitorTeamId, request.Date);
            var reviewsDto = reviews.Value.Select(r => _gameReviewMapper.GameReviewToGameReviewDto(r, null)).ToList();

            return new PagedResponse<IReadOnlyList<GameReviewDto>>
            {
                Success = true,
                Data = reviewsDto,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = reviews.TotalCount,
            };
        }
    }
}

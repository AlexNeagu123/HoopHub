using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetFanReviewsPaged
{
    public class GetFanGameReviewsPagedQueryHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetFanGameReviewsPagedQuery, PagedResponse<IReadOnlyList<GameReviewDto>>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly GameReviewMapper _gameReviewMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<PagedResponse<IReadOnlyList<GameReviewDto>>> Handle(GetFanGameReviewsPagedQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetFanGameReviewsPagedQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<IReadOnlyList<GameReviewDto>>.ErrorResponseFromFluentResult(validationResult);

            var fanId = _currentUserService.GetUserId!;
            if (request.FanId != null)
                fanId = request.FanId;

            var reviews = await _gameReviewRepository.GetAllPagedByFanIdAsync(request.Page, request.PageSize, fanId);
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

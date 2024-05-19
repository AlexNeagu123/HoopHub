using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetOwnReviewsPaged
{
    public class GetOwnGameReviewsPagedQuery : IRequest<PagedResponse<IReadOnlyList<GameReviewDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

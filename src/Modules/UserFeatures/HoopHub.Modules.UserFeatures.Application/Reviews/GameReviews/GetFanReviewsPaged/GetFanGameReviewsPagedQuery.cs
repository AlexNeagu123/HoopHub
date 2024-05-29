using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetFanReviewsPaged
{
    public class GetFanGameReviewsPagedQuery : IRequest<PagedResponse<IReadOnlyList<GameReviewDto>>>
    {
        public string? FanId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

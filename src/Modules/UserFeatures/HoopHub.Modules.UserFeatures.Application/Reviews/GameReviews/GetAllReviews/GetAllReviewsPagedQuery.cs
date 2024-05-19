using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetAllReviews
{
    public class GetAllReviewsPagedQuery : IRequest<PagedResponse<IReadOnlyList<GameReviewDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}

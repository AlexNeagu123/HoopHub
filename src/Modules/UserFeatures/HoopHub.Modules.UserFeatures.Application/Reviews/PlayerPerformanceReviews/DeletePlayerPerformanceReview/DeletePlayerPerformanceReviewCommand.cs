using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.DeletePlayerPerformanceReview
{
    public class DeletePlayerPerformanceReviewCommand : IRequest<BaseResponse>
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public Guid PlayerId { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}

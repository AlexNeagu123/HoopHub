using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.DeleteGameReview
{
    public class DeleteGameReviewCommand : IRequest<BaseResponse>
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}

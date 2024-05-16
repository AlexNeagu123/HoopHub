using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.DeleteGameReview
{
    public class DeleteGameReviewCommand : IRequest<BaseResponse>
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}

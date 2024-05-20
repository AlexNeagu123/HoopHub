using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.CreatePlayerPerformanceReview
{
    public class CreatePlayerPerformanceReviewCommand : IRequest<Response<PlayerPerformanceReviewDto>>
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public Guid PlayerId { get; set; }
        public string Date { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}

using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.GetPlayerPerformanceReview
{
    public class GetPlayerPerformanceReviewQuery : IRequest<Response<PlayerPerformanceReviewDto>>
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public Guid PlayerId { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}

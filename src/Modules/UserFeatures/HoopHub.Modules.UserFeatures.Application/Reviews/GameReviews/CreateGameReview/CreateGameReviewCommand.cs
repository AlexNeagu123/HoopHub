using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.CreateGameReview
{
    public class CreateGameReviewCommand : IRequest<Response<GameReviewDto>>
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}

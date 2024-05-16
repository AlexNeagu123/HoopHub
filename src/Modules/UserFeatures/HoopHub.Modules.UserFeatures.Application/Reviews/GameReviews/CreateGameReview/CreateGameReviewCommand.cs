using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.CreateGameReview
{
    public class CreateGameReviewCommand : IRequest<Response<GameReviewDto>>
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}

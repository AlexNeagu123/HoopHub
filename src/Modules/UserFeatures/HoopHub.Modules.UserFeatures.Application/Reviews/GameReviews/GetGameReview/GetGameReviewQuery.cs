using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReview
{
    public class GetGameReviewQuery : IRequest<Response<GameReviewDto>>
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = null!;
    }
}

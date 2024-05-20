using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReviewsByDate
{
    public class GetGameReviewsByDateQuery : IRequest<Response<IReadOnlyList<GameReviewAverageDto>>> 
    {
        public string Date { get; set; } = string.Empty;
    }
}

using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Reviews;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers
{
    public class GameReviewMapper
    {
        private readonly FanMapper _fanMapper = new();
        public GameReviewDto GameReviewToGameReviewDto(GameReview gameReview, decimal? averageRating)
        {
            return new GameReviewDto
            {
                Fan = _fanMapper.FanToFanDto(gameReview.Fan),
                HomeTeamId = gameReview.HomeTeamId,
                VisitorTeamId = gameReview.VisitorTeamId,
                Rating = gameReview.Rating,
                Date = gameReview.Date,
                AverageRating = averageRating,
                Content = gameReview.Content
            };
        }
    }
}

using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Reviews;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers
{
    public class PlayerPerformanceReviewMapper
    {
        private readonly FanMapper _fanMapper = new();

        public PlayerPerformanceReviewDto PlayerPerformanceReviewToPlayerPerformanceReviewDto(
            PlayerPerformanceReview playerPerformanceReview)
        {
            return new PlayerPerformanceReviewDto
            {
                Fan = _fanMapper.FanToFanDto(playerPerformanceReview.Fan),
                HomeTeamId = playerPerformanceReview.HomeTeamId,
                VisitorTeamId = playerPerformanceReview.VisitorTeamId,
                Rating = playerPerformanceReview.Rating,
                PlayerId = playerPerformanceReview.PlayerId,
                Date = playerPerformanceReview.Date
            };
        }
    }
}

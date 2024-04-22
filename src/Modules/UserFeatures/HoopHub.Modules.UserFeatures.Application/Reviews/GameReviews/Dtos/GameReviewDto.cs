using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos
{
    public class GameReviewDto
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public FanDto? Fan { get; set; }
        public decimal? Rating { get; set; }
        public decimal? AverageRating { get; set; }
    }
}

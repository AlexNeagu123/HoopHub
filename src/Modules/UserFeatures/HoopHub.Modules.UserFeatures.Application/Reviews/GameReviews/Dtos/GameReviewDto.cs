using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos
{
    public class GameReviewDto
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public FanDto? Fan { get; set; }
        public decimal? Rating { get; set; }
        public decimal? AverageRating { get; set; }
        public string? Content { get; set; } = string.Empty;
    }
}

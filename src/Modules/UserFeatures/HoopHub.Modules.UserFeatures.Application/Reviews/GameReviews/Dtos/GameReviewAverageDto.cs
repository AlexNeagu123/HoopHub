namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos
{
    public class GameReviewAverageDto
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public decimal? AverageRating { get; set; }
    }
}

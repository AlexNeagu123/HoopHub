namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos
{
    public class PlayerPerformanceAverageDto
    {
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public string Date { get; set; } = string.Empty;
        public Guid PlayerId { get; set; }
        public decimal? AverageRating { get; set; }
        public decimal? OwnRating { get; set; }
    }
}

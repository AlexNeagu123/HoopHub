using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos
{
    public class PlayerPerformanceReviewDto
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public string Date { get; set; }
        public Guid PlayerId { get; set; }
        public FanDto? Fan { get; set; }
        public decimal? Rating { get; set; }
    }
}

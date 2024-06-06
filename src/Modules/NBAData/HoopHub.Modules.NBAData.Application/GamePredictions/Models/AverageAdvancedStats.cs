namespace HoopHub.Modules.NBAData.Application.GamePredictions.Models
{
    public class AverageAdvancedStats
    {
        public int PlayerId { get; set; }
        public float Pie { get; set; } = 0;
        public float Pace { get; set; } = 0;
        public float AssistPercentage { get; set; } = 0;
        public float AssistRatio { get; set; } = 0;
        public float AssistToTurnover { get; set; } = 0;
        public float DefensiveRating { get; set; } = 0;
        public float DefensiveReboundPercentage { get; set; } = 0;
        public float EffectiveFieldGoalPercentage { get; set; } = 0;
        public float NetRating { get; set; } = 0;
        public float OffensiveRating { get; set; } = 0;
        public float OffensiveReboundPercentage { get; set; } = 0;
        public float ReboundPercentage { get; set; } = 0;
        public float TrueShootingPercentage { get; set; } = 0;
        public float TurnoverRatio { get; set; } = 0;
        public float UsagePercentage { get; set; } = 0;
    }
}

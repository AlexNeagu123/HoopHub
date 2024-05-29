using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData
{
    public class AdvancedStatsApiDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("pie")]
        public float? Pie { get; set; }

        [JsonProperty("pace")]
        public float? Pace { get; set; }

        [JsonProperty("assist_percentage")]
        public float? AssistPercentage { get; set; }

        [JsonProperty("assist_ratio")]
        public float? AssistRatio { get; set; }

        [JsonProperty("assist_to_turnover")]
        public float? AssistToTurnover { get; set; }

        [JsonProperty("defensive_rating")]
        public float? DefensiveRating { get; set; }

        [JsonProperty("defensive_rebound_percentage")]
        public float? DefensiveReboundPercentage { get; set; }

        [JsonProperty("effective_field_goal_percentage")]
        public float? EffectiveFieldGoalPercentage { get; set; }

        [JsonProperty("net_rating")]
        public float? NetRating { get; set; }

        [JsonProperty("offensive_rating")]
        public float? OffensiveRating { get; set; }

        [JsonProperty("offensive_rebound_percentage")]
        public float? OffensiveReboundPercentage { get; set; }

        [JsonProperty("rebound_percentage")]
        public float? ReboundPercentage { get; set; }

        [JsonProperty("true_shooting_percentage")]
        public float? TrueShootingPercentage { get; set; }

        [JsonProperty("turnover_ratio")]
        public float? TurnoverRatio { get; set; }

        [JsonProperty("usage_percentage")]
        public float? UsagePercentage { get; set; }

        [JsonProperty("player")]
        public AdvancedStatsPlayerApiDto Player { get; set; } = null!;

        [JsonProperty("team")]
        public AdvancedStatsTeamApiDto Team { get; set; } = null!;

        [JsonProperty("game")]
        public AdvancedStatsGameApiDto Game { get; set; } = null!;
    }
}
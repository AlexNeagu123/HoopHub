using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData
{
    public class AdvancedStatsGameApiDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; } = null!;

        [JsonProperty("season")]
        public int? Season { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("period")]
        public int? Period { get; set; }

        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("postseason")]
        public bool? Postseason { get; set; }

        [JsonProperty("home_team_score")]
        public int HomeTeamScore { get; set; }

        [JsonProperty("visitor_team_score")]
        public int VisitorTeamScore { get; set; }

        [JsonProperty("home_team_id")]
        public int HomeTeamId { get; set; }

        [JsonProperty("visitor_team_id")]
        public int VisitorTeamId { get; set; }
    }
}
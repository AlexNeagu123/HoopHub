using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData
{
    public class AdvancedStatsPlayerApiDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("position")]
        public string? Position { get; set; }

        [JsonProperty("height")]
        public string? Height { get; set; }

        [JsonProperty("weight")]
        public string? Weight { get; set; }

        [JsonProperty("jersey_number")]
        public string? JerseyNumber { get; set; }

        [JsonProperty("college")]
        public string? College { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("draft_year")]
        public int? DraftYear { get; set; }

        [JsonProperty("draft_round")]
        public int? DraftRound { get; set; }

        [JsonProperty("draft_number")]
        public int? DraftNumber { get; set; }

        [JsonProperty("team_id")]
        public int TeamId { get; set; }
    }
}
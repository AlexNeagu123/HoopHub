using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData
{
    public class PlayerApiDto
    {
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }

        [JsonProperty("jersey_number")]
        public string? JerseyNumber { get; set; }
        public string? College { get; set; }
        public string? Country { get; set; }

        [JsonProperty("draft_year")]
        public string? DraftYear { get; set; }

        [JsonProperty("draft_round")]
        public string? DraftRound { get; set; }

        [JsonProperty("draft_number")]
        public string? DraftNumber { get; set; }
    }
}

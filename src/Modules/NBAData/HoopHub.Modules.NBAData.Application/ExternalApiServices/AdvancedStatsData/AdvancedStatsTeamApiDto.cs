using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData
{
    public class AdvancedStatsTeamApiDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("conference")]
        public string? Conference { get; set; }

        [JsonProperty("division")]
        public string? Division { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("full_name")]
        public string? FullName { get; set; }

        [JsonProperty("abbreviation")]
        public string? Abbreviation { get; set; }
    }
}
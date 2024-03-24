using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData
{
    public class BoxScoreTeamApiDto
    {
        public int Id { get; set; }
        public string? Conference { get; set; }
        public string? Division { get; set; }
        public string? City { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Abbreviation { get; set; }

        [JsonProperty("players")]
        public List<BoxScorePlayerApiDto> Players { get; set; } = [];
    }
}

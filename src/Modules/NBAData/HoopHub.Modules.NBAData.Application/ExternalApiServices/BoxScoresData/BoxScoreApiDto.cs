using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData
{
    public class BoxScoreApiDto
    {
        public string? Date { get; set; }
        public int Season { get; set; }
        public string? Status { get; set; }
        public int Period { get; set; }
        public string? Time { get; set; }
        public bool Postseason { get; set; }

        [JsonProperty("home_team_score")]
        public int HomeTeamScore { get; set; }

        [JsonProperty("visitor_team_score")]
        public int VisitorTeamScore { get; set; }

        [JsonProperty("home_team")]
        public BoxScoreTeamApiDto? HomeTeam { get; set; }

        [JsonProperty("visitor_team")]
        public BoxScoreTeamApiDto? VisitorTeam { get; set; }
    }
}

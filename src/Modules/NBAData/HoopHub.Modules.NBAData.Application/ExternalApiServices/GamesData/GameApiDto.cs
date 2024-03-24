using HoopHub.Modules.NBAData.Application.Seasons;
using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData
{
    public class GameApiDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Season { get; set; }
        public int Period { get; set; }
        public string Time { get; set; } = string.Empty;
        public bool Postseason { get; set; }

        [JsonProperty("home_team_score")]
        public int HomeTeamScore { get; set; }

        [JsonProperty("visitor_team_score")]
        public int VisitorTeamScore { get; set; }

        [JsonProperty("home_team")]
        public TeamApiDto HomeTeam { get; set; }

        [JsonProperty("visitor_team")] 
        public TeamApiDto VisitorTeam { get; set; }
    }
}

using HoopHub.Modules.NBAData.Application.Seasons;
using HoopHub.Modules.NBAData.Application.Teams;

namespace HoopHub.Modules.NBAData.Application.Games
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Season { get; set; }
        public int Period { get; set; }
        public string Time { get; set; } = string.Empty;
        public bool Postseason { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitorTeamScore { get; set; }
        public TeamDto HomeTeam { get; set; }
        public TeamDto VisitorTeam { get; set; }
    }
}

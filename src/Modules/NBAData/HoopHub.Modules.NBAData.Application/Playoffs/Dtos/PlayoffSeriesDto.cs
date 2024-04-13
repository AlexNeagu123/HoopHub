using HoopHub.Modules.NBAData.Application.Seasons.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;

namespace HoopHub.Modules.NBAData.Application.Playoffs.Dtos
{
    public class PlayoffSeriesDto
    {
        public SeasonDto Season { get; set; }
        public TeamDto WinningTeam { get;  set; }
        public TeamDto LosingTeam { get;  set; }
        public int WinningTeamWins { get; set; }
        public int LosingTeamWins { get; set; }
        public string Stage { get;  set; }
        public int WinningTeamRank { get; set; }
        public int LosingTeamRank { get; set; }
    }
}

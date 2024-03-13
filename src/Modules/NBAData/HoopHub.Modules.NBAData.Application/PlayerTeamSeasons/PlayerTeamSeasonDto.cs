using HoopHub.Modules.NBAData.Application.Seasons;
using HoopHub.Modules.NBAData.Application.Teams;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons
{
    public class PlayerTeamSeasonDto
    {
        public SeasonDto Season { get; set; }
        public TeamDto Team { get; set; }
    }
}

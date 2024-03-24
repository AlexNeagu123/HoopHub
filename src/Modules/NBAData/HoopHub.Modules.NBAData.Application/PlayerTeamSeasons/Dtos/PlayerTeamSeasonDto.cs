using HoopHub.Modules.NBAData.Application.Seasons.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Dtos
{
    public class PlayerTeamSeasonDto
    {
        public SeasonDto Season { get; set; }
        public TeamDto Team { get; set; }
    }
}

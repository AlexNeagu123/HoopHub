using HoopHub.Modules.NBAData.Application.Seasons;
using HoopHub.Modules.NBAData.Application.Teams;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons
{
    public class PlayerTeamSeasonMapper
    {
        private readonly TeamMapper _teamMapper = new();
        private readonly SeasonMapper _seasonMapper = new();

        public PlayerTeamSeasonDto PlayerTeamSeasonToPlayerTeamSeasonDto(PlayerTeamSeason playerTeamSeason)
        {
            return new PlayerTeamSeasonDto
            {
                Team = _teamMapper.TeamToTeamDto(playerTeamSeason.Team),
                Season = _seasonMapper.SeasonToSeasonDto(playerTeamSeason.Season)
            };
        }
    }
}

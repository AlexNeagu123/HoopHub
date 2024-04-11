using HoopHub.Modules.NBAData.Application.Standings.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using HoopHub.Modules.NBAData.Domain.Standings;
using SeasonMapper = HoopHub.Modules.NBAData.Application.Seasons.Mappers.SeasonMapper;

namespace HoopHub.Modules.NBAData.Application.Standings.Mappers
{
    public class StandingsMapper
    {
        private readonly TeamMapper _teamMapper = new();
        private readonly SeasonMapper _seasonMapper = new();

        public StandingsEntryDto StandingsEntryToStandingsEntryDto(StandingsEntry entry)
        {
            return new StandingsEntryDto
            {
                Rank = entry.Rank,
                Overall = entry.Overall,
                Home = entry.Home,
                Road = entry.Road,
                EasternRecord = entry.EasternRecord,
                WesternRecord = entry.WesternRecord,
                Team = _teamMapper.TeamToTeamDto(entry.Team),
                Season = _seasonMapper.SeasonToSeasonDto(entry.Season)
            };
        }
    }
}

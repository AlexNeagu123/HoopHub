using HoopHub.Modules.NBAData.Application.Playoffs.Dtos;
using HoopHub.Modules.NBAData.Application.Seasons.Mappers;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using HoopHub.Modules.NBAData.Domain.Standings;

namespace HoopHub.Modules.NBAData.Application.Playoffs.Mappers
{
    public class PlayoffSeriesMapper
    {
        private readonly TeamMapper _teamMapper = new();
        private readonly SeasonMapper _seasonMapper = new();

        public PlayoffSeriesDto PlayoffSeriesToPlayoffSeriesDto(PlayoffSeries series, bool isLicensed)
        {
            return new PlayoffSeriesDto
            {
                Stage = series.Stage,
                WinningTeamRank = series.WinningTeamRank,
                LosingTeamRank = series.LosingTeamRank,
                WinningTeamWins = series.WinningTeamWins,
                LosingTeamWins = series.LosingTeamWins,
                LosingTeam = _teamMapper.TeamToTeamDto(series.LosingTeam, isLicensed),
                WinningTeam = _teamMapper.TeamToTeamDto(series.WinningTeam, isLicensed),
                Season = _seasonMapper.SeasonToSeasonDto(series.Season)
            };
        }
    }
}

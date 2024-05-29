using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Seasons.Mappers;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;
using HoopHub.Modules.NBAData.Domain.Games;

namespace HoopHub.Modules.NBAData.Application.Games.Mappers
{
    public class LocalGameMapper
    {
        private readonly TeamMapper _teamMapper = new();
        private readonly SeasonMapper _seasonMapper = new();

        public LocalStoredGameDto LocalStoredGameToLocalStoredGameDto(Game game, bool isLicensed)
        {
            return new LocalStoredGameDto
            {
                Id = game.Id,
                Date = game.Date,
                HomeTeam = game.HomeTeam != null ? _teamMapper.TeamToTeamDto(game.HomeTeam, isLicensed) : null,
                VisitorTeam = game.VisitorTeam != null ? _teamMapper.TeamToTeamDto(game.VisitorTeam, isLicensed) : null,
                Season = game.Season != null ? _seasonMapper.SeasonToSeasonDto(game.Season) : null,
                HomeTeamScore = game.HomeTeamScore,
                VisitorTeamScore = game.VisitorTeamScore,
                Status = game.Status,
                Period = game.Period,
                Time = game.Time,
                Postseason = game.Postseason
            };
        }
    }
}

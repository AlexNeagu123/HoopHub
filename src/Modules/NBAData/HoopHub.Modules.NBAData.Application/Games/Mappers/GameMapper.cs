using HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;

namespace HoopHub.Modules.NBAData.Application.Games.Mappers
{
    public class GameMapper
    {
        public GameDto GameApiDtoToGameDto(GameApiDto gameApiDto, TeamDto homeTeam, TeamDto visitorTeam)
        {
            return new GameDto
            {
                Id = gameApiDto.Id,
                Date = gameApiDto.Date,
                Season = gameApiDto.Season,
                Time = gameApiDto.Time,
                HomeTeamScore = gameApiDto.HomeTeamScore,
                VisitorTeamScore = gameApiDto.VisitorTeamScore,
                Period = gameApiDto.Period,
                Postseason = gameApiDto.Postseason,
                HomeTeam = homeTeam,
                VisitorTeam = visitorTeam
            };
        }
    }
}

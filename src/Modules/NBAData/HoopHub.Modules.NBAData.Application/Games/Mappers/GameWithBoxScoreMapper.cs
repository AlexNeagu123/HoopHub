using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;

namespace HoopHub.Modules.NBAData.Application.Games.Mappers
{
    public class GameWithBoxScoreMapper
    {
        public GameWithBoxScoreDto BoxScoreApiDtoToGameWithBoxScoreDto(BoxScoreApiDto boxScoreApi)
        {
            return new GameWithBoxScoreDto
            {
                Date = boxScoreApi.Date,
                Season = boxScoreApi.Season,
                Status = boxScoreApi.Status,
                Period = boxScoreApi.Period,
                Time = boxScoreApi.Time,
                Postseason = boxScoreApi.Postseason,
                HomeTeamScore = boxScoreApi.HomeTeamScore,
                VisitorTeamScore = boxScoreApi.VisitorTeamScore
            };
        }
    }
}

using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;

namespace HoopHub.Modules.NBAData.Application.Games.Mappers
{
    public class BoxScorePlayerMapper
    {
        public BoxScorePlayerDto BoxScoreApiPlayerDtoToBoxScorePlayerDto(BoxScorePlayerApiDto player)
        {
            return new BoxScorePlayerDto
            {
                Min = player.Min,
                Pts = player.Pts,
                Reb = player.Reb,
                Ast = player.Ast,
                Stl = player.Stl,
                Blk = player.Blk,
                Turnover = player.Turnover,
                Fgm = player.Fgm,
                Fga = player.Fga,
                FgPct = player.FgPct,
                Fg3m = player.Fg3m,
                Fg3a = player.Fg3a,
                Fg3Pct = player.Fg3Pct,
                Ftm = player.Ftm,
                Fta = player.Fta,
                FtPct = player.FtPct,
                Oreb = player.Oreb,
                Dreb = player.Dreb,
                Pf = player.Pf,
            };
        }
    }
}

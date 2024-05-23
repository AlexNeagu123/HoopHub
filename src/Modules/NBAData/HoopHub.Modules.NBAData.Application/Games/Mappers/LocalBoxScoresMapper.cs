using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Players.Mappers;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;

namespace HoopHub.Modules.NBAData.Application.Games.Mappers
{
    public class LocalBoxScoresMapper
    {
        private readonly LocalGameMapper _gameMapper = new();
        private readonly PlayerMapper _playerMapper = new();
        private readonly TeamMapper _teamMapper = new();
        public LocalStoredBoxScoresDto LocalStoredBoxScoresToLocalStoredBoxScoresDto(
            Domain.BoxScores.BoxScores boxScores)
        {
            return new LocalStoredBoxScoresDto
            {
                Id = boxScores.Id,
                Game = _gameMapper.LocalStoredGameToLocalStoredGameDto(boxScores.Game),
                Player = _playerMapper.PlayerToPlayerDto(boxScores.Player),
                Team = _teamMapper.TeamToTeamDto(boxScores.Team),
                Min = boxScores.Min,
                Fgm = boxScores.Fgm,
                Fga = boxScores.Fga,
                FgPct = boxScores.FgPct,
                Fg3m = boxScores.Fg3m,
                Fg3a = boxScores.Fg3a,
                Fg3Pct = boxScores.Fg3Pct,
                Ftm = boxScores.Ftm,
                Fta = boxScores.Fta,
                FtPct = boxScores.FtPct,
                Oreb = boxScores.Oreb,
                Dreb = boxScores.Dreb,
                Reb = boxScores.Reb,
                Ast = boxScores.Ast,
                Stl = boxScores.Stl,
                Blk = boxScores.Blk,
                Turnover = boxScores.Turnover,
                Pf = boxScores.Pf,
                Pts = boxScores.Pts
            };
        }
    }
}

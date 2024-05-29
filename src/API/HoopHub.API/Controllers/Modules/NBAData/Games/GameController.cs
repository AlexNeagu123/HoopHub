using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByGame;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByPlayer;
using HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoreByGame;
using HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByPlayer;
using HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam;
using HoopHub.Modules.NBAData.Application.Games.GetAllGamesByDate;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.NBAData.Games
{
    [Route("api/v1/nba-data/games")]
    public class GameController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGamesByDate([FromQuery] string date)
        {
            var response = await Mediator.Send(new GetAllGamesByDateQuery { Date = date });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("box-score")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBoxScoreByGame([FromQuery] string date, [FromQuery] int homeTeamId, [FromQuery] int visitorTeamId)
        {
            var response = await Mediator.Send(new GetBoxScoreByGameQuery
            { Date = date, HomeTeamApiId = homeTeamId, VisitorTeamApiId = visitorTeamId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("advanced-stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdvancedStatsByGame([FromQuery] string date, [FromQuery] int homeTeamId,
            [FromQuery] int visitorTeamId)
        {
            var response = await Mediator.Send(new GetAdvancedStatsByGameQuery
            { Date = date, HomeTeamApiId = homeTeamId, VisitorTeamApiId = visitorTeamId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGamesByTeam(Guid teamId, [FromQuery] int gameCount)
        {
            var response = await Mediator.Send(new GetBoxScoresByTeamQuery { TeamId = teamId, GameCount = gameCount });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("box-scores/{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBoxScoresByPlayer(Guid playerId, [FromQuery] int gameCount)
        {
            var response = await Mediator.Send(new GetBoxScoresByPlayerQuery { PlayerId = playerId, GameCount = gameCount });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("advanced-stats/{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdvancedStatsEntriesByPlayer(Guid playerId)
        {
            var response = await Mediator.Send(new GetAdvancedStatsEntriesByPlayerQuery { PlayerId = playerId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

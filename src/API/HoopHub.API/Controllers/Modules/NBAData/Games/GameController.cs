using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Games.GetAllGamesByDate;
using HoopHub.Modules.NBAData.Application.Games.GetBoxScoreByGame;
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
    }
}

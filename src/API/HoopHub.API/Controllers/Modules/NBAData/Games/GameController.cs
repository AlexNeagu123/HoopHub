using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Games.GetAllGamesByDate;
using HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam;
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
    }
}

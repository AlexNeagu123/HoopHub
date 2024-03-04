using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Players;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.NBAData
{
    [Route("api/nba-data/players")]
    public class PlayerController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PlayerDto>>> GetAllPlayers()
        {
            var response = await Mediator.Send(new GetAllPlayersQuery());
            return Ok(response);
        }
    }
}

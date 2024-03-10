using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.NBAData.Players
{
    [Route("api/v1/nba-data/players")]
    public class PlayerController : BaseApiController
    {
        [HttpGet("/active/by-team/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPlayers(Guid id)
        {
            var response = await Mediator.Send(new GetActivePlayersByTeamQuery { TeamId = id });
            return Ok(response);
        }
    }
}

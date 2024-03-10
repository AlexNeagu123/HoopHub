using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Teams.GetAllTeams;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.NBAData.Teams
{
    [Route("api/v1/nba-data/teams")]
    public class TeamController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeams()
        {
            var response = await Mediator.Send(new GetAllTeamsQuery());
            return Ok(response);
        }
    }
}

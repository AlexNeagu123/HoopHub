using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Standings.GetStandingsBySeason;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.NBAData.Standings
{
    [Route("api/v1/nba-data/standings")]
    public class StandingsController : BaseApiController
    {
        [HttpGet("season")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStandingsBySeason([FromQuery] int season)
        {
            var response = await Mediator.Send(new GetStandingsBySeasonQuery { Season= season });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

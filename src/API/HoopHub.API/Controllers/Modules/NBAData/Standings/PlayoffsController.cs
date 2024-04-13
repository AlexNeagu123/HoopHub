using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.Playoffs.GetPlayoffSeriesBySeason;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.NBAData.Standings
{
    [Route("api/v1/nba-data/playoffs")]
    public class PlayoffsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayoffSeriesBySeason([FromQuery] int season)
        {
            var response = await Mediator.Send(new GetPlayoffSeriesBySeasonQuery { Season = season });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

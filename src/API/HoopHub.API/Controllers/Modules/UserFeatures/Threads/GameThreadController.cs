using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Threads.GetGameThreadsByDate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Threads
{
    [Route("api/v1/user-features/game-threads")]
    public class GameThreadController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGameThreadsByDate([FromQuery] GetGameThreadsByDateQuery query)
        {
            var response = await Mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Fans.GetFanProfile;
using HoopHub.Modules.UserFeatures.Application.Fans.UpdateFan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Fans
{
    [Route("api/v1/user-features/fans")]
    public class FanController : BaseApiController
    {
        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateFanProfile([FromForm] UpdateFanCommand request)
        {
            var response = await Mediator.Send(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFanProfile([FromQuery] GetFanProfileQuery command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

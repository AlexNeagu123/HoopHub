using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.GetNotifications;
using HoopHub.Modules.UserFeatures.Application.FanNotifications.MarkNotificationAsRead;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Notifications
{
    [Route("api/v1/user-features/notifications")]
    public class NotificationController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsQuery query)
        {
            var response = await Mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> MarkNotificationAsRead(Guid id)
        {
            var response = await Mediator.Send(new MarkNotificationAsReadCommand { Id = id });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.CreatePlayerFollowEntry;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.DeletePlayerFollowEntry;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.GetPlayersFollowed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Follows
{
    [Route("api/v1/user-features/player-follows")]
    public class PlayerFollowEntriesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetPlayerFollowList()
        {
            var response = await Mediator.Send(new GetPlayersFollowedQuery());
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> FollowPlayer([FromBody] CreatePlayerFollowEntryCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UnfollowPlayer(Guid playerId)
        {
            var response = await Mediator.Send(new DeletePlayerFollowEntryCommand {PlayerId = playerId});
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

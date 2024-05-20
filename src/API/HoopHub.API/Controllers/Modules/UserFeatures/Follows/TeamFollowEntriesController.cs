using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.CreateTeamFollowEntry;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.DeleteTeamFollowEntry;
using HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.GetTeamsFollowed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Follows
{
    [Route("api/v1/user-features/team-follows")]
    public class TeamFollowEntriesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetPlayerFollowList()
        {
            var response = await Mediator.Send(new GetTeamsFollowedQuery());
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> FollowTeam([FromBody] CreateTeamFollowEntryCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UnfollowTeam(Guid teamId)
        {
            var response = await Mediator.Send(new DeleteTeamFollowEntryCommand {TeamId = teamId});
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

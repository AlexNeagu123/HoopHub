using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThreadVote;
using HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThreadVote;
using HoopHub.Modules.UserFeatures.Application.Threads.UpdateTeamThreadVote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Threads
{
    [Route("api/v1/user-features/thread-votes")]
    public class TeamThreadVotesController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> VoteThread([FromBody] CreateTeamThreadVoteCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteVoteThread(Guid id)
        {
            var response = await Mediator.Send(new DeleteTeamThreadVoteCommand { ThreadId = id });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateVoteThread(Guid id, [FromBody] UpdateTeamThreadVoteCommand command)
        {
            if (command.ThreadId != id)
                return BadRequest();

            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

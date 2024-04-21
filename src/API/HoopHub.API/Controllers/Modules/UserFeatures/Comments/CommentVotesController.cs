using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadCommentVote;
using HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadCommentVote;
using HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadCommentVote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Comments
{
    [Route("api/v1/user-features/comment-votes")]
    public class CommentVotesController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> VoteComment([FromBody] CreateThreadCommentVoteCommand command)
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
        public async Task<IActionResult> DeleteVoteComment(Guid id)
        {
            var response = await Mediator.Send(new DeleteThreadCommentVoteCommand { CommentId = id });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateVoteComment(Guid id, [FromBody] UpdateThreadCommentVoteCommand command)
        {
            if (command.CommentId != id)
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

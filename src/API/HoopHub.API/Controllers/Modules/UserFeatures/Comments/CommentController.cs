using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadComment;
using HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadCommentVote;
using HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadReplyComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Comments
{
    [Route("api/v1/user-features/comments")]
    public class CommentController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateComment([FromBody] CreateThreadCommentCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("reply")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReplyComment([FromBody] CreateThreadReplyCommentCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("vote")]
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
    }
}

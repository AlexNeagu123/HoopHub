using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadComment;
using HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadReplyComment;
using HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadComment;
using HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByThread;
using HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByUserId;
using HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Comments
{
    [Route("api/v1/user-features/comments")]
    public class CommentController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComments([FromQuery] GetCommentsPagedByThreadQuery query)
        {
            var response = await Mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("fan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentsByUser([FromQuery] GetCommentsPagedByUserIdQuery query)
        {
            var response = await Mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

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

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var response = await Mediator.Send(new DeleteThreadCommentCommand { CommentId = id });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateThreadCommentCommand command)
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

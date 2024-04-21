using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.CreateGameReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.DeleteGameReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.UpdateGameReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.CreatePlayerPerformanceReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.DeletePlayerPerformanceReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.UpdatePlayerPerformanceReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Reviews
{
    [Route("api/v1/user-features/reviews")]
    public class ReviewsController : BaseApiController
    {
        [HttpPost("player-performance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> CreatePlayerReview([FromBody] CreatePlayerPerformanceReviewCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("player-performance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> DeletePlayerReview([FromQuery] DeletePlayerPerformanceReviewCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("player-performance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdatePlayerReview([FromBody] UpdatePlayerPerformanceReviewCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> CreateGameReview([FromBody] CreateGameReviewCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> DeleteGameReview([FromQuery] DeleteGameReviewCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdateGameReview([FromBody] UpdateGameReviewCommand command)
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

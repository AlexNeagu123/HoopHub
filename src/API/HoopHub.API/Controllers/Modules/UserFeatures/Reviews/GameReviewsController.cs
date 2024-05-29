using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.CreateGameReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.DeleteGameReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetAllReviews;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetFanReviewsPaged;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReview;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReviewsByDate;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.UpdateGameReview;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserFeatures.Reviews
{
    [Route("api/v1/user-features/game-reviews")]
    public class GameReviewsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetGameReview([FromQuery] GetGameReviewQuery command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
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

        [HttpDelete]
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

        [HttpPut]
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

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGameReviewsPaged([FromQuery] GetAllReviewsPagedQuery command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("by-date")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGameReviewsPagedByDate([FromQuery] GetGameReviewsByDateQuery command)
        {
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("fans")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGameReviewsPagedByFanId(
            [FromQuery] GetFanGameReviewsPagedQuery command)
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

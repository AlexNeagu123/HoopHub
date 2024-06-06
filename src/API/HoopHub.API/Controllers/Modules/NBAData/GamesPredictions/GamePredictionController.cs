using HoopHub.BuildingBlocks.API;
using HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.NBAData.GamesPredictions
{
    [Route("api/v1/nba-data/game-prediction")]
    public class GamePredictionController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PredictGame([FromQuery] GetGamePredictionQuery request)
        {

            var response = await Mediator.Send(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

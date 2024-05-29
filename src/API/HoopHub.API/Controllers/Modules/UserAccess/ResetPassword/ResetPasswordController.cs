using HoopHub.Modules.UserAccess.Application.Services.ResetPassword;
using HoopHub.Modules.UserAccess.Domain.ResetPassword;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserAccess.ResetPassword
{
    [Route("api/v1/reset-password")]
    [ApiController]

    public class ResetPasswordController(IResetPasswordService resetPasswordService) : ControllerBase
    {
        private readonly IResetPasswordService _resetPasswordService = resetPasswordService;

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail([FromQuery] string? email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _resetPasswordService.SendResetPasswordEmail(email);
            if (response.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }

        [HttpPatch]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _resetPasswordService.ResetPasswordAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }
    }
}

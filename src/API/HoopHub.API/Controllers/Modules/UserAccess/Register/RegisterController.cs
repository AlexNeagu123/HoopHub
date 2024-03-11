using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.Registration;
using HoopHub.Modules.UserAccess.Domain.Roles;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserAccess.Register
{
    [Route("api/v1/register")]
    [ApiController]
    public class RegisterController(IRegistrationService registrationService) : ControllerBase
    {
        private readonly IRegistrationService _registrationService = registrationService;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _registrationService.RegisterAsync(request, UserRoles.User);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

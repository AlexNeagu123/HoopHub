using HoopHub.Modules.UserAccess.Application.Services.Login;
using HoopHub.Modules.UserAccess.Domain.Login;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserAccess.Login
{
    [Route("api/v1/login")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _loginService.LoginAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }
    }
}

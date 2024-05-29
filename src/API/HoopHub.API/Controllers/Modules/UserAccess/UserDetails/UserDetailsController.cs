using HoopHub.Modules.UserAccess.Application.Services.UserDetails;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserAccess.Domain.UserDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoopHub.API.Controllers.Modules.UserAccess.UserDetails
{
    [Route("api/v1/user-details")]
    [ApiController]
    public class UserDetailsController(IUserDetailsService userDetailsService) : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService = userDetailsService;

        [HttpPatch]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UserDetailsModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userDetailsService.UpdateUserDetails(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }
    }
}

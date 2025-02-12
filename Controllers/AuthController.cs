using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Services;

namespace SmartScheduler.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (!Enum.IsDefined(request.Role))
            {
                return BadRequest("Invalid role.");
            }

            var user = await authService.RegisterUserAsync(request);

            if (user is null)
            {
                return BadRequest("Username already exists.");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
           var response = await authService.LoginAsync(request);

            if (response is null) {
                return BadRequest("Invalid username or password.");
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null )
            {
                return Unauthorized("Invalid refresh token");
            }

            return Ok(result);
        }
    }
}

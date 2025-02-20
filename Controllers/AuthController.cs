using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Services.Contracts;
using System.Net;

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
                throw new ClientException("Invalid role.", HttpStatusCode.BadRequest);
            }

            var user = await authService.RegisterUserAsync(request);

            if (user is null)
            {
                throw new ClientException("Username already exists.", HttpStatusCode.BadRequest);
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
           var response = await authService.LoginAsync(request);

            if (response is null)
            {
                throw new ClientException("Invalid username or password.", HttpStatusCode.BadRequest);
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null )
            {
                throw new ClientException("Invalid refresh token", HttpStatusCode.Unauthorized);
            }

            return Ok(result);
        }
    }
}

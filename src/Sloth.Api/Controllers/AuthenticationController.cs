using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sloth.Auth.Models;
using Sloth.Auth;
using System.Security.Claims;

namespace Sloth.Api.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService) {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthResponse> Login([FromBody]IdentityModel model)
        {
           return await _authService.LoginAsync(model);
            
        }
        [AllowAnonymous]
        [HttpPost("logon")]
        public async Task<Guid> Logon([FromBody]RegisterModel model)
        {
            return await _authService.LogonAsync(model);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<AuthResponse> Refresh([FromBody]RefreshModel model)
        {
            return await _authService.RefreshAsync(model);
        }

        [HttpGet("logout")]
        public async Task Logout([FromBody]RefreshModel model)
        {
            await _authService.LogoutAsync(new Guid(HttpContext.User.FindFirstValue(ClaimTypes.Sid)));
        }
    }
}
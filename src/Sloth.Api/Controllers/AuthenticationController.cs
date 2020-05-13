using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Extensions;
using Sloth.Auth;
using Sloth.Auth.Models;
using System;
using System.Threading.Tasks;

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
        public async Task<AuthResponse> LoginAsync([FromBody]IdentityModel model)
        {
           return await _authService.LoginAsync(model);
            
        }
        [AllowAnonymous]
        [HttpPost("logon")]
        public async Task<Guid> LogonAsync([FromBody]RegisterModel model)
        {
            return await _authService.LogonAsync(model);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<AuthResponse> RefreshAsync([FromBody]RefreshModel model)
        {
            return await _authService.RefreshAsync(model);
        }

        [HttpGet("logout")]
        public async Task LogoutAsync([FromBody]RefreshModel model)
        {
            await _authService.LogoutAsync(HttpContext.GetCurrentUserId());
        }
    }
}
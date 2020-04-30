using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sloth.DB.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Sloth.Auth.Models;
using Sloth.Auth;
using System.Security.Claims;

namespace Sloth.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthenticationController(IMapper mapper, IAuthService authService) {
            _mapper = mapper;
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

        [HttpGet("current")]
        public async Task<CurrentUser> Current()
        { 
            return await _authService.GetCurrentUserAsync(new Guid(HttpContext.User.FindFirstValue(ClaimTypes.Sid)));
        }
    }
}
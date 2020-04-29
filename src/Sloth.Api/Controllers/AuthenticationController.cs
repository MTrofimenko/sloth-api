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

namespace Sloth.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IAuthService _authServise;

        public AuthenticationController(IMapper mapper, IAuthService authServise) {
            _mapper = mapper;
            _authServise = authServise;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthResponse> Login([FromBody]IdentityModel model)
        {
           return await _authServise.Login(model);
            
        }
        [AllowAnonymous]
        [HttpPost("logon")]
        public async Task<Guid> Logon([FromBody]RegisterModel model)
        {
            return await _authServise.Logon(model);
        }

        [HttpGet("current")]
        public async Task<CurrentUser> Current()
        {
            return await _authServise.GetCurrentUser(HttpContext.User.Identity.Name);
        }
    }
}
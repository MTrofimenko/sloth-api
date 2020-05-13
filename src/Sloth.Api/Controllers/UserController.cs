using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Services;
using Sloth.Auth.Models;

namespace Sloth.Api
{
    [Authorize]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("current")]
        public async Task<CurrentUser> Current()
        {
           return await _userService.GetCurrentUserAsync(new Guid(HttpContext.User.FindFirstValue(ClaimTypes.Sid)));
        }
    }
}
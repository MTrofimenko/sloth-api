﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Extensions;
using Sloth.Api.Services;
using Sloth.Auth.Models;
using System.Threading.Tasks;

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
        public async Task<CurrentUser> GetCurrentUserAsync()
        {
           return await _userService.GetCurrentUserAsync(HttpContext.GetCurrentUserId());
        }
    }
}
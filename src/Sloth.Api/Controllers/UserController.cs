using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Extensions;
using Sloth.Api.Models;
using Sloth.Api.Services;

namespace Sloth.Api.Controllers
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
        public async Task<UserModel> GetCurrentUserAsync()
        {
           return await _userService.GetCurrentUserAsync(HttpContext.GetCurrentUserId());
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsersAsync(string namePart)
        {
            return await _userService.GetUsersByNameAsync(namePart);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sloth.DB;
using Sloth.DB.Models;

namespace Sloth.Api
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ISlothDbContext _dbContext;

        public UserController(ISlothDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET
        [HttpGet]
        public async Task<User[]> Register()
        {
            var users = await _dbContext.Users.ToArrayAsync();
            return users;
        }
    }
}
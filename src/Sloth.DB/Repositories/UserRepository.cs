using Microsoft.EntityFrameworkCore;
using Sloth.DB.Models;
using System;
using System.Threading.Tasks;

namespace Sloth.DB.Repositories
{
    public class UserRepository: BaseRepository, IUserRepository 
    {
        public UserRepository(ISlothDbContext dbContext): base(dbContext) { }
        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task<bool> AnyByLoginAsync(string login)
        {
            return await DbContext.Users.AnyAsync(x => x.Login == login);
        }
        public async Task<Guid> AddAsync(User user)
        {
           await DbContext.Users.AddAsync(user);
           await DbContext.SaveChangesAsync();
           return user.Id;
        }
    }
}

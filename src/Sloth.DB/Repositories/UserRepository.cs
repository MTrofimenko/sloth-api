using Microsoft.EntityFrameworkCore;
using Sloth.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sloth.DB.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const int MAX_SELECT_COUNT = 10;
        public UserRepository(ISlothDbContext dbContext) : base(dbContext)
        {
        }

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

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string namePart)
        {
            var users = await DbContext.Users.Where(x =>
                    x.FirstName.Contains(namePart) || 
                    x.LastName.Contains(namePart) || 
                    x.Login.Contains(namePart))
                .Take(MAX_SELECT_COUNT)
                .ToArrayAsync();
            return users;
        }
    }
}
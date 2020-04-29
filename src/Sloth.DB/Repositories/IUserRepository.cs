using Sloth.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sloth.DB.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLoginAsync(string login);
        Task<bool> AnyByLoginAsync(string login);
        Task<Guid> AddAsync(User user);
    }
}

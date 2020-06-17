using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sloth.Api.Models;

namespace Sloth.Api.Services
{
    public interface IUserService
    {
        Task<UserModel> GetCurrentUserAsync(Guid userId);
        Task<IEnumerable<UserModel>> GetUsersByNameAsync(string namePart);
    }
}
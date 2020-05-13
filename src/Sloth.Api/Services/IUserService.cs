using Sloth.Auth.Models;
using System;
using System.Threading.Tasks;

namespace Sloth.Api.Services
{
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync(Guid guid);
    }
}
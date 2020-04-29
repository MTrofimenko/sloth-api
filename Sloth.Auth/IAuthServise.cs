using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Sloth.Auth.Models;

namespace Sloth.Auth
{
    public interface IAuthService
    {
        Task<Guid> Logon(RegisterModel model);
        Task<AuthResponse> Login(IdentityModel model);
        Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken);
        Task<CurrentUser> GetCurrentUser(string name);
    }
}
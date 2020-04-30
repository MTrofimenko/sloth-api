using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Sloth.Auth.Models;

namespace Sloth.Auth
{
    public interface IAuthService
    {
        Task<Guid> LogonAsync(RegisterModel model);
        Task<AuthResponse> LoginAsync(IdentityModel model);
        Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken);
        Task<CurrentUser> GetCurrentUserAsync(Guid id);
    }
}
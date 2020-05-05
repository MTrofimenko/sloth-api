using Microsoft.AspNetCore.Mvc;
using Sloth.Auth.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sloth.Auth.AuthProviders
{
    public interface IAuthProvider
    {
        Task<AuthResponse> LoginAsync(IdentityModel model);
        Task<Guid> LogonAsync(RegisterModel model);
        Task LogoutAsync();
        Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken);
        Task<CurrentUser> GetCurrentUserAsync(Guid id);
    }
}

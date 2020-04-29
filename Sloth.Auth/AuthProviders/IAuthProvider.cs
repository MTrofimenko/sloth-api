using Microsoft.AspNetCore.Mvc;
using Sloth.Auth.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sloth.Auth.AuthProviders
{
    public interface IAuthProvider
    {
        Task<AuthResponse> Login(IdentityModel model);
        Task<Guid> Logon(RegisterModel model);
        Task<IActionResult> Logout();
        Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken);
        Task<CurrentUser> GetCurrentUser(string name);
    }
}

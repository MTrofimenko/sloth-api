using Sloth.Auth.AuthProviders;
using Sloth.Auth.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sloth.Auth
{
    public class AuthServise : IAuthService
    {
        private readonly IAuthProvider _authProvider;

        public AuthServise(IAuthProvider authProvider) {
            _authProvider = authProvider;
        }

        public async Task<CurrentUser> GetCurrentUser(string name)
        {
            return await _authProvider.GetCurrentUser(name);
        }

        public async Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken)
        {
            return await _authProvider.IntrospectTokenAsync(authenticationToken);
        }

        public async Task<AuthResponse> Login(IdentityModel model)
        {
            return await _authProvider.Login(model);
        }

        public async Task<Guid> Logon(RegisterModel model)
        {
            return await _authProvider.Logon(model);
        }
    }
}

using Sloth.Auth.AuthProviders;
using Sloth.Auth.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sloth.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthProvider _authProvider;

        public AuthService(IAuthProvider authProvider) {
            _authProvider = authProvider;
        }

        public async Task<CurrentUser> GetCurrentUserAsync(Guid id)
        {
            return await _authProvider.GetCurrentUserAsync(id);
        }

        public async Task<ClaimsPrincipal> IntrospectTokenAsync(string authenticationToken)
        {
            return await _authProvider.IntrospectTokenAsync(authenticationToken);
        }

        public async Task<AuthResponse> LoginAsync(IdentityModel model)
        {
            return await _authProvider.LoginAsync(model);
        }

        public async Task<Guid> LogonAsync(RegisterModel model)
        {
            return await _authProvider.LogonAsync(model);
        }
    }
}

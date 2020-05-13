using Sloth.Auth.AuthProviders;
using Sloth.Auth.Models;
using System;
using System.Threading.Tasks;

namespace Sloth.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthProvider _authProvider;

        public AuthService(IAuthProvider authProvider) {
            _authProvider = authProvider;
        }

        public async Task<AuthResponse> LoginAsync(IdentityModel model)
        {
            return await _authProvider.LoginAsync(model);
        }

        public async Task<Guid> LogonAsync(RegisterModel model)
        {
            return await _authProvider.LogonAsync(model);
        }
        public async  Task<AuthResponse> RefreshAsync(RefreshModel model)
        {
            return await _authProvider.RefreshAsync(model);
        }
        public async Task LogoutAsync(Guid userId)
        {
            await _authProvider.LogoutAsync(userId);
        }
    }
}

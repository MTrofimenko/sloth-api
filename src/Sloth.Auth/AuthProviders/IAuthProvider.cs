using Sloth.Auth.Models;
using System;
using System.Threading.Tasks;

namespace Sloth.Auth.AuthProviders
{
    public interface IAuthProvider
    {
        Task<AuthResponse> LoginAsync(IdentityModel model);
        Task<Guid> LogonAsync(RegisterModel model);
        Task LogoutAsync(Guid userId);
        Task<AuthResponse> RefreshAsync(RefreshModel model);
    }
}

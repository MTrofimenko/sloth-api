using Sloth.Auth.Models;
using System;
using System.Threading.Tasks;

namespace Sloth.Auth
{
    public interface IAuthService
    {
        Task<Guid> LogonAsync(RegisterModel model);
        Task<AuthResponse> LoginAsync(IdentityModel model);
        Task<AuthResponse> RefreshAsync(RefreshModel model);
        Task LogoutAsync(Guid userId);
    }
}
using Sloth.DB.Models;
using System;
using System.Threading.Tasks;

namespace Sloth.DB.Repositories
{
    public interface ISessionRefreshTokenRepository
    {
        Task<Guid> AddAsync(SessionRefreshToken token);
        Task<SessionRefreshToken> GetSessionByTokenAsync(string token);
        Task<SessionRefreshToken> UpdateAsync(SessionRefreshToken token);
        Task<SessionRefreshToken[]> UpdateRangeAsync(SessionRefreshToken[] tokens);
        Task<SessionRefreshToken[]> GetAllActiveTokenByUserIdAsync(Guid userId);
    }
}
using Microsoft.EntityFrameworkCore;
using Sloth.DB.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sloth.DB.Repositories
{
    public class SessionRefreshTokenRepository : BaseRepository, ISessionRefreshTokenRepository
    {
        public SessionRefreshTokenRepository(ISlothDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Guid> AddAsync(SessionRefreshToken token)
        {
            await DbContext.SessionRefreshTokens.AddAsync(token);
            await DbContext.SaveChangesAsync();
            return token.Id;
        }

        public async Task<SessionRefreshToken> GetSessionByTokenAsync(string token)
        {
            return await DbContext.SessionRefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<SessionRefreshToken> UpdateAsync(SessionRefreshToken token)
        {
            DbContext.SessionRefreshTokens.Update(token);
            await DbContext.SaveChangesAsync();
            return token;
        }
        public async Task<SessionRefreshToken[]> UpdateRangeAsync(SessionRefreshToken[] tokens)
        {
            DbContext.SessionRefreshTokens.UpdateRange(tokens);
            await DbContext.SaveChangesAsync();
            return tokens;
        }
        public async Task<SessionRefreshToken[]> GetAllActiveTokenByUserIdAsync(Guid userId)
        {
            return await DbContext.SessionRefreshTokens.Where(x=> x.UserId == userId && x.IsActive).ToArrayAsync();
        }
    }
}

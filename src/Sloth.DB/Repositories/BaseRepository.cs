
namespace Sloth.DB.Repositories
{
    public class BaseRepository
    {
        protected ISlothDbContext DbContext { get;}

        public BaseRepository(ISlothDbContext dbContext) {
            DbContext = dbContext;
        }
    }
}

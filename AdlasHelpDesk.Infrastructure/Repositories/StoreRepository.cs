using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

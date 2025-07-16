using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class PurchaseSourceRepository : BaseRepository<PurchaseSource>, IPurchaseSourceRepository
    {
        public PurchaseSourceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

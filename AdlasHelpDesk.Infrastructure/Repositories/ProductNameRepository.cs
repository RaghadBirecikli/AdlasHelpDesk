using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class ProductNameRepository : BaseRepository<ProductName>, IProductNameRepository
    {
        public ProductNameRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

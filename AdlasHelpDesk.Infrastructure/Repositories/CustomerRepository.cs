using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

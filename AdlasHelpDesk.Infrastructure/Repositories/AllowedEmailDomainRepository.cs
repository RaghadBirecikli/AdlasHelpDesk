using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class AllowedEmailDomainRepository : BaseRepository<AllowedEmailDomain>, IAllowedEmailDomainRepository
    {
        public AllowedEmailDomainRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

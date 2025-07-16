using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class TicketStatusRepository : BaseRepository<TicketStatus>, ITicketStatusRepository
    {
        public TicketStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

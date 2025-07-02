using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

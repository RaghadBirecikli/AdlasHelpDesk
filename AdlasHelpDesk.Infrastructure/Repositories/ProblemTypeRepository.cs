using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class ProblemTypeRepository : BaseRepository<ProblemType>, IProblemTypeRepository
    {
        public ProblemTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

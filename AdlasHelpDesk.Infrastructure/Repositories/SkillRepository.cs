using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

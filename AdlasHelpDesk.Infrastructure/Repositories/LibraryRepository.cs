using AdlasHelpDesk.Infrastructure.Repositories.Base;

namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

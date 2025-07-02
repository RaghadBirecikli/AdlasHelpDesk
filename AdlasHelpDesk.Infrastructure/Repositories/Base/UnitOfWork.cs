using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}


namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class SendMailRepository : BaseRepository<SendMail>, ISendMailRepository
    {
        public SendMailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
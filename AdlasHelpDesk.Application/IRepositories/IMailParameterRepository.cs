
namespace AdlasHelpDesk.Application.IRepositories
{
    public interface IMailParameterRepository : IBaseRepository<MailParameter>
    {
        public SmtpModel GetSmtp();
    }
}
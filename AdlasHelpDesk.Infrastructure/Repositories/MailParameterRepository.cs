
namespace AdlasHelpDesk.Infrastructure.Repositories
{

    public class MailParameterRepository : BaseRepository<MailParameter>, IMailParameterRepository
    {
        public MailParameterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SmtpModel GetSmtp()
        {
            return new SmtpModel
            {
                Server = "smtp.Adlas.com", // ? ???? ??? ?????
                Port = 587,
                User = "info@adlas.com",
                Password = "YourStrongPasswordHere",
                UserAlias = "Adlas Help Desk",
                SSL = true,
                Timeout = 10000 // ??????? ????? (10 ?????)
            };
        }
    }
}
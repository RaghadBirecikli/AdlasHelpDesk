
namespace AdlasHelpDesk.Infrastructure.Repositories
{
    public class MailParameterRepository : BaseRepository<MailParameter>, IMailParameterRepository
    {
        ApplicationDbContext _context;

        public MailParameterRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        string password = "SMTPPassword";
        string server = "SMTPServer";
        string user = "SMTPUser";
        string port = "SMTPPort";
        string userAlias = "SMTPUserAlias";
        string timeout = "SMTPTimeout";
        string SSL = "SMTPSSL";
        public SmtpModel GetSmtp()
        {
            SmtpModel smtpModel = new SmtpModel();
            var list = _context.MailParameter;
            smtpModel.Password= list.SingleOrDefault(x => x.Code == password).Value;
            smtpModel.Server = list.SingleOrDefault(x => x.Code == server).Value;
            smtpModel.User = list.SingleOrDefault(x => x.Code == user).Value;
            smtpModel.Port = Convert.ToInt16(list.SingleOrDefault(x => x.Code == port).Value);
            smtpModel.UserAlias = list.SingleOrDefault(x => x.Code == userAlias).Value;
            smtpModel.SSL = list.SingleOrDefault(x => x.Code == SSL).Value == "0";
            smtpModel.Timeout = Convert.ToInt16(list.SingleOrDefault(x => x.Code == timeout).Value);
            return smtpModel;
        }
    }
}
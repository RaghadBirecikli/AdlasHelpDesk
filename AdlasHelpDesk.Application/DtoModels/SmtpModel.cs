
namespace AdlasHelpDesk.Application.DtoModels
{
    public class SmtpModel
    {
        public SmtpModel()
        {

        }
        public SmtpModel(string password, string server, string user, int port, string userAlias, int timeout, bool sSL)
        {
            Password = password;
            Server = server;
            User = user;
            Port = port;
            UserAlias = userAlias;
            Timeout = timeout;
            SSL = sSL;
        }

        public string Password { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public int Port { get; set; }
        public string UserAlias { get; set; }
        public int Timeout { get; set; }
        public bool SSL { get; set; }
    }
}
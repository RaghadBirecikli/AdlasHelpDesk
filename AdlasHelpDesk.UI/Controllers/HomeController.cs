using AdlasHelpDesk.Application.IRepositories;
using AdlasHelpDesk.Application.UserHelpers;
using AdlasHelpDesk.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace AdlasHelpDesk.UI.Controllers
{
    [Route("home")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberService _memberService;
        private readonly ICompanyService _companyService;
        private readonly IMailParameterRepository _mailParameterRepository;
        private readonly ISendMailService _sendMailService;
        private readonly IStringLocalizer<HomeController> _localizer;
        readonly private INotyfService _notyf;

        public HomeController(ILogger<HomeController> logger, IMemberService memberService, ICompanyService companyService, IMailParameterRepository mailParameterRepository, ISendMailService sendMailService, IStringLocalizer<HomeController> localizer, INotyfService notyf)
        {
            _logger = logger;
            _memberService = memberService;
            _companyService = companyService;
            _mailParameterRepository = mailParameterRepository;
            _sendMailService = sendMailService;
            _localizer = localizer;
            _notyf = notyf;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet, Route("[action]")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
        //public bool SendContactMail(ContactVM model, string reciverEmail)
        //{
        //    SmtpModel smtp = _mailParameterRepository.GetSmtp();
        //    var newLine = "<br>";
        //    string emailHead = "<b>Contact-Us Email</b>" + newLine;
        //    string mail = emailHead + "<b>G?nderim Tarihi :</b>" + DateTime.Now + newLine +
        //             "<b>Ad           :</b>" + model.SenderName + newLine +
        //             "<b>E-Mail           :</b>" + model.SenderEmail + newLine + model.Message;

        //    return Functions.SendEmail(smtp, mail, model.Subject, reciverEmail, null);

        //}
    }
}

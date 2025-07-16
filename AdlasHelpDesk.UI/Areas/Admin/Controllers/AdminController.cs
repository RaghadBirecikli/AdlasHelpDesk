using AdlasHelpDesk.Application.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IMemberService _memberService;
        private readonly ICurrentUserService _currentUserService;

        public AdminController(INotyfService notyf, IMemberService memberService, ICurrentUserService currentUserService)
        {
            _notyf = notyf;
            _memberService = memberService;
            _currentUserService = currentUserService;
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

        [Route("/MemberPanel")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

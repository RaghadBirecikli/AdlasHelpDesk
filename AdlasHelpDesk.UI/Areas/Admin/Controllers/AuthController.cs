using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdlasHelpDesk.Application.Filters;
using AdlasHelpDesk.Application.UserHelpers;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AuthController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;


        public AuthController(INotyfService notyf, IAuthService authService)
        {
            _notyf = notyf;
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnURL)
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> login(LoginFilter model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Login(model);
                if (response.Meta.IsSuccess)
                {
                    await HttpContext.SignInAsync(
                        Functions.CreateClaims(response.Entity.SignedMember),
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddDays(1).AddMinutes(-1),
                            IsPersistent = true
                        });

                    _notyf.Success(response.Meta.MessageDetail, 3);
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        return LocalRedirect(model.ReturnUrl);
                    return LocalRedirect("/MemberPanel");
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
                return View(model);
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}

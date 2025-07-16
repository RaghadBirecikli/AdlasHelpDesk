using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    public class PublisherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

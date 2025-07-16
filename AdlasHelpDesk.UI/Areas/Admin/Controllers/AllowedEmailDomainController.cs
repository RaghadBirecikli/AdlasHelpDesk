using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AllowedEmailDomainController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IAllowedEmailDomainService _allowedEmailDomainService;

        public AllowedEmailDomainController(INotyfService notyf, IAuthService authService, IAllowedEmailDomainService allowedEmailDomainService)
        {
            _notyf = notyf;
            _authService = authService;
            _allowedEmailDomainService = allowedEmailDomainService;
        }

        [Route("/AllowedEmailDomainList")]
        public async Task<IActionResult> Index(AllowedEmailDomainVM model)
        {
            ListResult<AllowedEmailDomainDto> response = await _allowedEmailDomainService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/AllowedEmailDomainCreate")]
        public IActionResult CreateAllowedEmailDomain()
        {
            AllowedEmailDomainUpsertDto model = new AllowedEmailDomainUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/AllowedEmailDomainCreate")]
        public async Task<IActionResult> CreateAllowedEmailDomain(AllowedEmailDomainUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<AllowedEmailDomainUpsertDto> response = await _allowedEmailDomainService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateAllowedEmailDomain), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/AllowedEmailDomainUpdate")]
        public async Task<IActionResult> UpdateAllowedEmailDomain(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<AllowedEmailDomainUpsertDto> response = await _allowedEmailDomainService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateAllowedEmailDomain", response.Entity);
        }
        [HttpPost, Route("/AllowedEmailDomainUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAllowedEmailDomain(AllowedEmailDomainUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<AllowedEmailDomainUpsertDto> updateRes = await _allowedEmailDomainService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateAllowedEmailDomain), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateAllowedEmailDomain", model);
        }
        public async Task<IActionResult> DeleteAllowedEmailDomain(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _allowedEmailDomainService.Delete(id);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Error(response.Meta.MessageDetail, 3);
                }
            }
            return RedirectToAction(nameof(UpdateAllowedEmailDomain), new { id = id });
        }
    }
}

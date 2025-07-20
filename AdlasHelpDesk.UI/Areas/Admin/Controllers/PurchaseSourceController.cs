using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class PurchaseSourceController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IPurchaseSourceService _PurchaseSourceService;

        public PurchaseSourceController(INotyfService notyf, IAuthService authService, IPurchaseSourceService PurchaseSourceService)
        {
            _notyf = notyf;
            _authService = authService;
            _PurchaseSourceService = PurchaseSourceService;
        }

        [Route("/PurchaseSourceList")]
        public async Task<IActionResult> Index(PurchaseSourceVM model)
        {
            ListResult<PurchaseSourceDto> response = await _PurchaseSourceService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/PurchaseSourceCreate")]
        public IActionResult CreatePurchaseSource()
        {
            PurchaseSourceUpsertDto model = new PurchaseSourceUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/PurchaseSourceCreate")]
        public async Task<IActionResult> CreatePurchaseSource(PurchaseSourceUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<PurchaseSourceUpsertDto> response = await _PurchaseSourceService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdatePurchaseSource), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/PurchaseSourceUpdate")]
        public async Task<IActionResult> UpdatePurchaseSource(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<PurchaseSourceUpsertDto> response = await _PurchaseSourceService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreatePurchaseSource", response.Entity);
        }
        [HttpPost, Route("/PurchaseSourceUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePurchaseSource(PurchaseSourceUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<PurchaseSourceUpsertDto> updateRes = await _PurchaseSourceService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdatePurchaseSource), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreatePurchaseSource", model);
        }
        public async Task<IActionResult> DeletePurchaseSource(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _PurchaseSourceService.Delete(id);
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
            return RedirectToAction(nameof(UpdatePurchaseSource), new { id = id });
        }
    }
}

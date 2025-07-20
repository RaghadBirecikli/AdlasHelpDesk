using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class StoreController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IStoreService _StoreService;

        public StoreController(INotyfService notyf, IAuthService authService, IStoreService StoreService)
        {
            _notyf = notyf;
            _authService = authService;
            _StoreService = StoreService;
        }

        [Route("/StoreList")]
        public async Task<IActionResult> Index(StoreVM model)
        {
            ListResult<StoreDto> response = await _StoreService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/StoreCreate")]
        public IActionResult CreateStore()
        {
            StoreUpsertDto model = new StoreUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/StoreCreate")]
        public async Task<IActionResult> CreateStore(StoreUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<StoreUpsertDto> response = await _StoreService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateStore), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/StoreUpdate")]
        public async Task<IActionResult> UpdateStore(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<StoreUpsertDto> response = await _StoreService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateStore", response.Entity);
        }
        [HttpPost, Route("/StoreUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStore(StoreUpsertDto model)
        {
          
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<StoreUpsertDto> updateRes = await _StoreService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateStore), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateStore", model);
        }
        public async Task<IActionResult> DeleteStore(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _StoreService.Delete(id);
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
            return RedirectToAction(nameof(UpdateStore), new { id = id });
        }
    }
}

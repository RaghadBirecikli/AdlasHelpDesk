using AdlasHelpDesk.Application.Filters;
using AdlasHelpDesk.Application.Results;
using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using AdlasHelpDesk.UI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class UniversityController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IUniversityService _universityService;

        public UniversityController(INotyfService notyf, IAuthService authService, IUniversityService universityService)
        {
            _notyf = notyf;
            _authService = authService;
            _universityService = universityService;
        }

        [Route("/UniversityList")]
        public async Task<IActionResult> Index(UniversityVM model)
        {
            UniversityFilter filter = new UniversityFilter();
            filter.PublicFilter = model.PublicFilter;

            ListResult<UniversityDto> response = await _universityService.GetAll(filter);

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/UniversityCreate")]
        public async Task<IActionResult> CreateUniversity()
        {
            UniversityUpsertDto model = new UniversityUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/UniversityCreate")]
        public async Task<IActionResult> CreateUniversity(UniversityUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<UniversityUpsertDto> response = await _universityService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateUniversity), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/UniversityUpdate")]
        public async Task<IActionResult> UpdateUniversity(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<UniversityUpsertDto> response = await _universityService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateUniversity", response.Entity);
        }
        [HttpPost, Route("/UniversityUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUniversity(UniversityUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<UniversityUpsertDto> updateRes = await _universityService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateUniversity), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateUniversity", model);
        }
        public async Task<IActionResult> DeleteUniversity(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _universityService.Delete(id);
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
            return RedirectToAction(nameof(UpdateUniversity), new { id = id });
        }
    }
}

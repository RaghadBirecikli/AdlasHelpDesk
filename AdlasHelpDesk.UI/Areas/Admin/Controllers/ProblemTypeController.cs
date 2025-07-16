using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProblemTypeController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IProblemTypeService _problemTypeService;

        public ProblemTypeController(INotyfService notyf, IAuthService authService, IProblemTypeService ProblemTypeService)
        {
            _notyf = notyf;
            _authService = authService;
            _problemTypeService = ProblemTypeService;
        }

        [Route("/ProblemTypeList")]
        public async Task<IActionResult> Index(ProblemTypeVM model)
        {
            ListResult<ProblemTypeDto> response = await _problemTypeService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/ProblemTypeCreate")]
        public IActionResult CreateProblemType()
        {
            ProblemTypeUpsertDto model = new ProblemTypeUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/ProblemTypeCreate")]
        public async Task<IActionResult> CreateProblemType(ProblemTypeUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<ProblemTypeUpsertDto> response = await _problemTypeService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProblemType), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/ProblemTypeUpdate")]
        public async Task<IActionResult> UpdateProblemType(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<ProblemTypeUpsertDto> response = await _problemTypeService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateProblemType", response.Entity);
        }
        [HttpPost, Route("/ProblemTypeUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProblemType(ProblemTypeUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<ProblemTypeUpsertDto> updateRes = await _problemTypeService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProblemType), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateProblemType", model);
        }
        public async Task<IActionResult> DeleteProblemType(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _problemTypeService.Delete(id);
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
            return RedirectToAction(nameof(UpdateProblemType), new { id = id });
        }
    }
}

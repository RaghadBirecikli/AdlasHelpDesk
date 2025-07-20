using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class SkillController : Controller
    {

        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly ISkillService _SkillService;
        private readonly IPublisherService _publisherService;

        public SkillController(INotyfService notyf, IAuthService authService, ISkillService SkillService, IPublisherService publisherService)
        {
            _notyf = notyf;
            _authService = authService;
            _SkillService = SkillService;
            _publisherService = publisherService;
        }

        public async Task<SelectList> fillPublishersList()
        {
            return new SelectList((await _publisherService.GetAll()).Entities, "Id", "Name");
        }
        [Route("/SkillList")]
        public async Task<IActionResult> Index(SkillVM model)
        {
            ListResult<SkillDto> response = await _SkillService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/SkillCreate")]
        public async Task<IActionResult> CreateSkill()
        {
            SkillUpsertVM model = new SkillUpsertVM();
            model.Publishers = await fillPublishersList();
            return View(model);
        }
        [HttpPost, Route("/SkillCreate")]
        public async Task<IActionResult> CreateSkill(SkillUpsertVM model)
        {
            ModelState.Remove("Publishers");
            if (ModelState.IsValid)
            {
                ObjectResult<SkillUpsertDto> response = await _SkillService.Add(model.SkillUpsertDto);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateSkill), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            model.Publishers = await fillPublishersList();
            return View(model);
        }
        [HttpGet, Route("/SkillUpdate")]
        public async Task<IActionResult> UpdateSkill(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<SkillUpsertDto> response = await _SkillService.Get(id);
            if (response.Entity == null)
                return NotFound();

            SkillUpsertVM model = new SkillUpsertVM();
            model.Publishers = await fillPublishersList();
            model.SkillUpsertDto = response.Entity;
            return View("CreateSkill", model);
        }
        [HttpPost, Route("/SkillUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSkill(SkillUpsertVM model)
        {
            ModelState.Remove("Publishers");
            if (ModelState.IsValid)
            {
                if (model.SkillUpsertDto.Id == null)
                    return BadRequest();

                ObjectResult<SkillUpsertDto> updateRes = await _SkillService.Update(model.SkillUpsertDto);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateSkill), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }
            model.Publishers = await fillPublishersList();
            return View("CreateSkill", model);
        }
        public async Task<IActionResult> DeleteSkill(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _SkillService.Delete(id);
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
            return RedirectToAction(nameof(UpdateSkill), new { id = id });
        }
    }
}

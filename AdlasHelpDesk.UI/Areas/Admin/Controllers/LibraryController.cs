using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class LibraryController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly ILibraryService _libraryService;

        public LibraryController(INotyfService notyf, IAuthService authService, ILibraryService LibraryService)
        {
            _notyf = notyf;
            _authService = authService;
            _libraryService = LibraryService;
        }

        [Route("/LibraryList")]
        public async Task<IActionResult> Index(LibraryVM model)
        {
            ListResult<LibraryDto> response = await _libraryService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/LibraryCreate")]
        public IActionResult CreateLibrary()
        {
            LibraryUpsertDto model = new LibraryUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/LibraryCreate")]
        public async Task<IActionResult> CreateLibrary(LibraryUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<LibraryUpsertDto> response = await _libraryService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateLibrary), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/LibraryUpdate")]
        public async Task<IActionResult> UpdateLibrary(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<LibraryUpsertDto> response = await _libraryService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateLibrary", response.Entity);
        }
        [HttpPost, Route("/LibraryUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLibrary(LibraryUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<LibraryUpsertDto> updateRes = await _libraryService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateLibrary), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateLibrary", model);
        }
        public async Task<IActionResult> DeleteLibrary(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _libraryService.Delete(id);
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
            return RedirectToAction(nameof(UpdateLibrary), new { id = id });
        }
    }
}

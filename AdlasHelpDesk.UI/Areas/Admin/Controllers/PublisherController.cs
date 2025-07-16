using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class PublisherController : Controller
    {
            readonly private INotyfService _notyf;
            private readonly IAuthService _authService;
            private readonly IPublisherService _PublisherService;

            public PublisherController(INotyfService notyf, IAuthService authService, IPublisherService PublisherService)
            {
                _notyf = notyf;
                _authService = authService;
                _PublisherService = PublisherService;
            }

            [Route("/PublisherList")]
            public async Task<IActionResult> Index(PublisherVM model)
            {
                ListResult<PublisherDto> response = await _PublisherService.GetAll();

                if (response.Meta.IsSuccess)
                {
                    model.Entities = response.Entities;
                    return View(model);
                }
                return NotFound();
            }

            [HttpGet, Route("/PublisherCreate")]
            public IActionResult CreatePublisher()
            {
                PublisherUpsertDto model = new PublisherUpsertDto();
                return View(model);
            }
            [HttpPost, Route("/PublisherCreate")]
            public async Task<IActionResult> CreatePublisher(PublisherUpsertDto model)
            {
                if (ModelState.IsValid)
                {
                    ObjectResult<PublisherUpsertDto> response = await _PublisherService.Add(model);
                    if (response.Meta.IsSuccess)
                    {
                        _notyf.Success(response.Meta.MessageDetail, 3);
                        return RedirectToAction(nameof(UpdatePublisher), new { id = response.Entity.Id });
                    }
                    _notyf.Error(response.Meta.MessageDetail, 3);
                }
                return View(model);
            }
            [HttpGet, Route("/PublisherUpdate")]
            public async Task<IActionResult> UpdatePublisher(Guid id)
            {
                if (id == null)
                    return BadRequest();

                ObjectResult<PublisherUpsertDto> response = await _PublisherService.Get(id);
                if (response.Entity == null)
                    return NotFound();

                return View("CreatePublisher", response.Entity);
            }
            [HttpPost, Route("/PublisherUpdate")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> UpdatePublisher(PublisherUpsertDto model)
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == null)
                        return BadRequest();

                    ObjectResult<PublisherUpsertDto> updateRes = await _PublisherService.Update(model);
                    if (updateRes.Meta.IsSuccess)
                    {
                        _notyf.Success(updateRes.Meta.MessageDetail, 3);
                        return RedirectToAction(nameof(UpdatePublisher), new { id = updateRes.Entity.Id });
                    }
                    else _notyf.Error(updateRes.Meta.MessageDetail, 3);
                }

                return View("CreatePublisher", model);
            }
            public async Task<IActionResult> DeletePublisher(Guid id)
            {

                Result response;
                if (id != null)
                {
                    response = await _PublisherService.Delete(id);
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
                return RedirectToAction(nameof(UpdatePublisher), new { id = id });
            }
        }
    }


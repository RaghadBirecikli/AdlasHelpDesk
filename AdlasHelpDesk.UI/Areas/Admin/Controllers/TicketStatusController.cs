using AdlasHelpDesk.Application.Filters;
using AdlasHelpDesk.Domain.Models;
using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class TicketStatusController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly ITicketStatusService _ticketStatusService;

        public TicketStatusController(INotyfService notyf, IAuthService authService, ITicketStatusService ticketStatusService)
        {
            _notyf = notyf;
            _authService = authService;
            _ticketStatusService = ticketStatusService;
        }

        [Route("/TicketStatusList")]
        public async Task<IActionResult> Index(TicketStatusVM model)
        {
            ListResult<TicketStatusDto> response = await _ticketStatusService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/TicketStatusCreate")]
        public  IActionResult CreateTicketStatus()
        {
            TicketStatusUpsertDto model = new TicketStatusUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/TicketStatusCreate")]
        public async Task<IActionResult> CreateTicketStatus(TicketStatusUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<TicketStatusUpsertDto> response = await _ticketStatusService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateTicketStatus), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/TicketStatusUpdate")]
        public async Task<IActionResult> UpdateTicketStatus(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<TicketStatusUpsertDto> response = await _ticketStatusService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateTicketStatus", response.Entity);
        }
        [HttpPost, Route("/TicketStatusUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTicketStatus(TicketStatusUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<TicketStatusUpsertDto> updateRes = await _ticketStatusService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateTicketStatus), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateTicketStatus", model);
        }
        public async Task<IActionResult> DeleteTicketStatus(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _ticketStatusService.Delete(id);
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
            return RedirectToAction(nameof(UpdateTicketStatus), new { id = id });
        }
    }
}

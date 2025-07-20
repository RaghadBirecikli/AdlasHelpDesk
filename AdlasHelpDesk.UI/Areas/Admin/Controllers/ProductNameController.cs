using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProductNameController : Controller
    {
        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IProductNameService _productNameService;
        private readonly IPublisherService _publisherService;

        public ProductNameController(INotyfService notyf, IAuthService authService, IProductNameService productNameService, IPublisherService publisherService)
        {
            _notyf = notyf;
            _authService = authService;
            _productNameService = productNameService;
            _publisherService = publisherService;
        }

        public async Task<SelectList> fillPublishersList()
        {
            return new SelectList((await _publisherService.GetAll()).Entities, "Id", "Name");
        }
        [Route("/ProductNameList")]
        public async Task<IActionResult> Index(ProductNameVM model)
        {
            ListResult<ProductNameDto> response = await _productNameService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/ProductNameCreate")]
        public async Task<IActionResult> CreateProductName()
        {
            ProductNameUpsertVM model = new ProductNameUpsertVM();
            model.Publishers=await fillPublishersList();
            return View(model);
        }
        [HttpPost, Route("/ProductNameCreate")]
        public async Task<IActionResult> CreateProductName(ProductNameUpsertVM model)
        {
            ModelState.Remove("Publishers");
            if (ModelState.IsValid)
            {
                ObjectResult<ProductNameUpsertDto> response = await _productNameService.Add(model.ProductNameUpsertDto);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProductName), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            model.Publishers = await fillPublishersList();
            return View(model);
        }
        [HttpGet, Route("/ProductNameUpdate")]
        public async Task<IActionResult> UpdateProductName(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<ProductNameUpsertDto> response = await _productNameService.Get(id);
            if (response.Entity == null)
                return NotFound();

            ProductNameUpsertVM model = new ProductNameUpsertVM();
            model.Publishers = await fillPublishersList();
            model.ProductNameUpsertDto = response.Entity;
            return View("CreateProductName", model);
        }
        [HttpPost, Route("/ProductNameUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductName(ProductNameUpsertVM model)
        {
            ModelState.Remove("Publishers");
            if (ModelState.IsValid)
            {
                if (model.ProductNameUpsertDto.Id == null)
                    return BadRequest();

                ObjectResult<ProductNameUpsertDto> updateRes = await _productNameService.Update(model.ProductNameUpsertDto);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProductName), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }
            model.Publishers = await fillPublishersList();
            return View("CreateProductName", model);
        }
        public async Task<IActionResult> DeleteProductName(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _productNameService.Delete(id);
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
            return RedirectToAction(nameof(UpdateProductName), new { id = id });
        }
    }
}

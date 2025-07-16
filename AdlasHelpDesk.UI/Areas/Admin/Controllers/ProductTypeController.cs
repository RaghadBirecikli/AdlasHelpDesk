using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProductTypeController : Controller
    {

        readonly private INotyfService _notyf;
        private readonly IAuthService _authService;
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(INotyfService notyf, IAuthService authService, IProductTypeService ProductTypeService)
        {
            _notyf = notyf;
            _authService = authService;
            _productTypeService = ProductTypeService;
        }

        [Route("/ProductTypeList")]
        public async Task<IActionResult> Index(ProductTypeVM model)
        {
            ListResult<ProductTypeDto> response = await _productTypeService.GetAll();

            if (response.Meta.IsSuccess)
            {
                model.Entities = response.Entities;
                return View(model);
            }
            return NotFound();
        }

        [HttpGet, Route("/ProductTypeCreate")]
        public IActionResult CreateProductType()
        {
            ProductTypeUpsertDto model = new ProductTypeUpsertDto();
            return View(model);
        }
        [HttpPost, Route("/ProductTypeCreate")]
        public async Task<IActionResult> CreateProductType(ProductTypeUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                ObjectResult<ProductTypeUpsertDto> response = await _productTypeService.Add(model);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProductType), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            return View(model);
        }
        [HttpGet, Route("/ProductTypeUpdate")]
        public async Task<IActionResult> UpdateProductType(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<ProductTypeUpsertDto> response = await _productTypeService.Get(id);
            if (response.Entity == null)
                return NotFound();

            return View("CreateProductType", response.Entity);
        }
        [HttpPost, Route("/ProductTypeUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductType(ProductTypeUpsertDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                    return BadRequest();

                ObjectResult<ProductTypeUpsertDto> updateRes = await _productTypeService.Update(model);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProductType), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }

            return View("CreateProductType", model);
        }
        public async Task<IActionResult> DeleteProductType(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _productTypeService.Delete(id);
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
            return RedirectToAction(nameof(UpdateProductType), new { id = id });
        }
    }
}


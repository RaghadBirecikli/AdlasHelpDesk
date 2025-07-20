using AdlasHelpDesk.UI.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace AdlasHelpDesk.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProductController : Controller
    {

        readonly private INotyfService _notyf;
        private readonly IProductService _ProductService;
        private readonly IPublisherService _publisherService;
        private readonly IProductTypeService _productTypeService;
        private readonly IProductNameService _productNameService;
        private readonly ISkillService _skillService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(INotyfService notyf, IProductService productService, IPublisherService publisherService, IProductTypeService productTypeService, IProductNameService productNameService, ISkillService skillService, IWebHostEnvironment hostEnvironment)
        {
            _notyf = notyf;
            _ProductService = productService;
            _publisherService = publisherService;
            _productTypeService = productTypeService;
            _productNameService = productNameService;
            _skillService = skillService;
            _hostEnvironment = hostEnvironment;
        }

        public async Task fillLists(ProductUpsertVM model)
        {
            model.Publishers = new SelectList((await _publisherService.GetAll()).Entities, "Id", "Name");
            model.ProductTypes = new SelectList((await _productTypeService.GetAll()).Entities, "Id", "Name");
            if (model.ProductUpsertDto?.PublisherId != null)
            {
                model.ProductNames = new SelectList((await _productNameService.GetProductNamesByPublisher(model.ProductUpsertDto.PublisherId)).Entities, "Id", "Name");
                model.Skills = new SelectList((await _skillService.GetSkillsByPublisher(model.ProductUpsertDto.PublisherId)).Entities, "Id", "Name");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsNamesAndSkillsByPublisherAsync(Guid publisherId)
        {
            var productNames = (await _productNameService.GetProductNamesByPublisher(publisherId)).Entities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            var skills = (await _skillService.GetSkillsByPublisher(publisherId)).Entities
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return Json(new { productNames, skills });
        }
       

        [Route("/ProductList")]
        public async Task<IActionResult> Index()
        {
            ListResult<ProductDto> response = await _ProductService.GetAll();
            if (response.Meta.IsSuccess)
            {

                return View(response.Entities);
            }
            return NotFound();
        }


        [HttpGet, Route("/ProductCreate")]
        public async Task<IActionResult> CreateProduct()
        {
            ProductUpsertVM model = new ProductUpsertVM();
            await fillLists(model);
            return View(model);
        }
        [HttpPost, Route("/ProductCreate")]
        public async Task<IActionResult> CreateProduct(ProductUpsertVM model)
        {
            ModelState.Remove("Publishers"); 
            ModelState.Remove("ProductTypes");
            ModelState.Remove("ProductNames");
            ModelState.Remove("Skills");
            if (ModelState.IsValid)
            {
                ObjectResult<ProductUpsertDto> response = await _ProductService.Add(model.ProductUpsertDto);
                if (response.Meta.IsSuccess)
                {
                    _notyf.Success(response.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProduct), new { id = response.Entity.Id });
                }
                _notyf.Error(response.Meta.MessageDetail, 3);
            }
            await fillLists(model);
            return View(model);
        }
        [HttpGet, Route("/ProductUpdate")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            if (id == null)
                return BadRequest();

            ObjectResult<ProductUpsertDto> response = await _ProductService.Get(id);
            if (response.Entity == null)
                return NotFound();

            //await fillProductUpsertDtoSelectLists(model);
            ProductUpsertVM model = new ProductUpsertVM();
            model.ProductUpsertDto = response.Entity;
            await fillLists(model);
            return View("CreateProduct", model);
        }
        [HttpPost, Route("/ProductUpdate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductUpsertVM model)
        {
            ModelState.Remove("Publishers");
            ModelState.Remove("ProductTypes");
            ModelState.Remove("ProductNames");
            ModelState.Remove("Skills");
            if (ModelState.IsValid)
            {
                if (model.ProductUpsertDto.Id == null)
                    return BadRequest();

                ObjectResult<ProductUpsertDto> updateRes = await _ProductService.Update(model.ProductUpsertDto);
                if (updateRes.Meta.IsSuccess)
                {
                    _notyf.Success(updateRes.Meta.MessageDetail, 3);
                    return RedirectToAction(nameof(UpdateProduct), new { id = updateRes.Entity.Id });
                }
                else _notyf.Error(updateRes.Meta.MessageDetail, 3);
            }
            await fillLists(model);
            return View("CreateProduct", model);
        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {

            Result response;
            if (id != null)
            {
                response = await _ProductService.Delete(id);
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
            return RedirectToAction(nameof(UpdateProduct), new { id = id });
        }
    }
}


using System.Reflection.Metadata;

namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _ProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
        private readonly string TableName = "Product";

        public ProductService(IStringLocalizer localizer, IMapper mapper, IProductRepository ProductRepository, IHostingEnvironment hostEnvironment, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _ProductRepository = ProductRepository;
            _hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }


        public async Task<ListResult<ProductDto>> GetAll()
        {
            return new ListResult<ProductDto>(Meta.Success(), _mapper.Map<List<ProductDto>>(
                    await _ProductRepository.GetListAsync(null, x=>x.ProductName, false)
                    ));
        }
        public async Task<ListResult<ProductDto>> GetList()
        {
            List<ProductDto> list = _mapper.Map<List<ProductDto>>(await _ProductRepository.GetListAsync(x => x.IsActive, x => x.ProductName, false));

            return new ListResult<ProductDto>(Meta.Success(), list);
        }
        public async Task<ObjectResult<ProductUpsertDto>> Get(Guid id)
        {
            Product? entity = await _ProductRepository.GetByIdAsync(id);
            if (entity is null)
                return new ObjectResult<ProductUpsertDto>(Meta.NotFound());
            ProductUpsertDto model = _mapper.Map<ProductUpsertDto>(entity);

            return new ObjectResult<ProductUpsertDto>(Meta.Success(), model);
        }
        public async Task<ObjectResult<ProductUpsertDto>> Add(ProductUpsertDto model)
        {
            if (await _ProductRepository.AnyAsync(p =>
        p.ProductNameId == model.ProductNameId &&
        p.PublisherId == model.PublisherId &&
        p.ProductTypeId == model.ProductTypeId &&
        p.SkillId == model.SkillId))
            {
                return new ObjectResult<ProductUpsertDto>(
                    Meta.CustomError(_localizer["RecordAlreadyExists"])
                );
            }
            else
            {
                var Product = _mapper.Map<Product>(model);
                Product.Id = Guid.NewGuid();

               if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                    string imagePath = await Functions.SaveImage(model.ImageFile, Product, _hostEnvironment);
                    Product.ImageUrl = imagePath;
                }
                Product = await _ProductRepository.AddAsync(Product);


                await _unitOfWork.CompleteAsync();
                return new ObjectResult<ProductUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<ProductUpsertDto>(Product));
            }
        }

        public async Task<ObjectResult<ProductUpsertDto>> Update(ProductUpsertDto model)
        {
            if (await _ProductRepository.AnyAsync(p => p.Id != model.Id && p.ProductNameId == model.ProductNameId &&
        p.PublisherId == model.PublisherId &&
        p.ProductTypeId == model.ProductTypeId &&
        p.SkillId == model.SkillId))
            {

                return new ObjectResult<ProductUpsertDto>(
                    Meta.CustomError(_localizer["RecordAlreadyExists"])
                );
            }


            Product? entity = await _ProductRepository.GetByIdAsync(model.Id.Value);
            if (entity is null)
                return new ObjectResult<ProductUpsertDto>(Meta.NotFound());

            entity.ProductTypeId = model.ProductTypeId;
            entity.PublisherId = model.PublisherId;
            entity.ProductNameId = model.ProductNameId;
            entity.SkillId = model.SkillId;
            entity.IsActive = model.IsActive;

            entity = _ProductRepository.Update(entity);

            if (model.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                    Functions.DeleteImage(entity.ImageUrl, entity, _hostEnvironment);

                string imagePath = await Functions.SaveImage(model.ImageFile, entity, _hostEnvironment);
                entity.ImageUrl = imagePath;
            }
            await _unitOfWork.CompleteAsync();
            return new ObjectResult<ProductUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<ProductUpsertDto>(entity));
        }

        public async Task<Result> Delete(Guid id)
        {
            Product? entity = await _ProductRepository.GetByIdAsync(id);
            if (entity is null)
                return new Result(Meta.NotFound());
            if (!string.IsNullOrEmpty(entity.ImageUrl))
                Functions.DeleteImage(entity.ImageUrl, entity, _hostEnvironment);
            _ProductRepository.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return new Result(Meta.CustomSuccess(ConstantMessages.RecordDeleted));
        }
    }
}

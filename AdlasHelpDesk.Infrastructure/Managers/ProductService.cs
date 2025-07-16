
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
                    await _ProductRepository.GetListAsync(null, null, false)
                    ));
        }
        public async Task<ListResult<ProductDto>> GetList()
        {
            List<ProductDto> list = _mapper.Map<List<ProductDto>>(await _ProductRepository.GetListAsync(x => x.IsActive, null, false));

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

                Product = await _ProductRepository.AddAsync(Product);

                await _unitOfWork.CompleteAsync();
                return new ObjectResult<ProductUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<ProductUpsertDto>(Product));
            }
        }
        public async Task<ObjectResult<ProductUpsertDto>> Update(ProductUpsertDto model)
        {
            Product? entity = await _ProductRepository.GetByIdAsync(model.Id.Value);
            if (entity is null)
                return new ObjectResult<ProductUpsertDto>(Meta.NotFound());

            entity.ProductTypeId = model.ProductTypeId;
            entity.PublisherId = model.PublisherId;
            entity.ProductNameId = model.ProductNameId;
            entity.SkillId = model.SkillId;
            entity.ImageUrl = model.ImageUrl;
            entity.IsActive = model.IsActive;

            entity = _ProductRepository.Update(entity);


            await _unitOfWork.CompleteAsync();

            return new ObjectResult<ProductUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<ProductUpsertDto>(entity));
        }
        public async Task<Result> Delete(Guid id)
        {
            Product? entity = await _ProductRepository.GetByIdAsync(id);
            if (entity is null)
                return new Result(Meta.NotFound());


            _ProductRepository.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
        }


    }
}

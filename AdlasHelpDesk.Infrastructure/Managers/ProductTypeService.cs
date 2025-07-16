
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class ProductTypeService : IProductTypeService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IProductTypeRepository _ProductTypeRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "ProductType";

		public ProductTypeService(IStringLocalizer localizer, IMapper mapper, IProductTypeRepository ProductTypeRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_ProductTypeRepository = ProductTypeRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<ProductTypeDto>> GetAll()
		{
			return new ListResult<ProductTypeDto>(Meta.Success(), _mapper.Map<List<ProductTypeDto>>(
					await _ProductTypeRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<ProductTypeDto>> GetList()
		{
			List<ProductTypeDto> list = _mapper.Map<List<ProductTypeDto>>(await _ProductTypeRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<ProductTypeDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<ProductTypeUpsertDto>> Get(Guid id)
		{
			ProductType? entity = await _ProductTypeRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<ProductTypeUpsertDto>(Meta.NotFound());
			ProductTypeUpsertDto model = _mapper.Map<ProductTypeUpsertDto>(entity);

			return new ObjectResult<ProductTypeUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<ProductTypeUpsertDto>> Add(ProductTypeUpsertDto model)
		{
			if (await _ProductTypeRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<ProductTypeUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var ProductType = _mapper.Map<ProductType>(model);
			ProductType.Id = Guid.NewGuid();

			ProductType = await _ProductTypeRepository.AddAsync(ProductType);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<ProductTypeUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<ProductTypeUpsertDto>(ProductType));
		}

		public async Task<ObjectResult<ProductTypeUpsertDto>> Update(ProductTypeUpsertDto model)
		{
			if (await _ProductTypeRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<ProductTypeUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			ProductType? entity = await _ProductTypeRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<ProductTypeUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _ProductTypeRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<ProductTypeUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<ProductTypeUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			ProductType? entity = await _ProductTypeRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_ProductTypeRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}


namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class StoreService : IStoreService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IStoreRepository _StoreRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "Store";

		public StoreService(IStringLocalizer localizer, IMapper mapper, IStoreRepository StoreRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_StoreRepository = StoreRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<StoreDto>> GetAll()
		{
			return new ListResult<StoreDto>(Meta.Success(), _mapper.Map<List<StoreDto>>(
					await _StoreRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<StoreDto>> GetList()
		{
			List<StoreDto> list = _mapper.Map<List<StoreDto>>(await _StoreRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<StoreDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<StoreUpsertDto>> Get(Guid id)
		{
			Store? entity = await _StoreRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<StoreUpsertDto>(Meta.NotFound());
			StoreUpsertDto model = _mapper.Map<StoreUpsertDto>(entity);

			return new ObjectResult<StoreUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<StoreUpsertDto>> Add(StoreUpsertDto model)
		{
			if (await _StoreRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<StoreUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var Store = _mapper.Map<Store>(model);
			Store.Id = Guid.NewGuid();

			Store = await _StoreRepository.AddAsync(Store);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<StoreUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<StoreUpsertDto>(Store));
		}

		public async Task<ObjectResult<StoreUpsertDto>> Update(StoreUpsertDto model)
		{
			if (await _StoreRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<StoreUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			Store? entity = await _StoreRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<StoreUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _StoreRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<StoreUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<StoreUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			Store? entity = await _StoreRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_StoreRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}

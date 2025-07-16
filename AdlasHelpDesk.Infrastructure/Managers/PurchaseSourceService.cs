
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class PurchaseSourceService : IPurchaseSourceService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IPurchaseSourceRepository _PurchaseSourceRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "PurchaseSource";

		public PurchaseSourceService(IStringLocalizer localizer, IMapper mapper, IPurchaseSourceRepository PurchaseSourceRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_PurchaseSourceRepository = PurchaseSourceRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<PurchaseSourceDto>> GetAll()
		{
			return new ListResult<PurchaseSourceDto>(Meta.Success(), _mapper.Map<List<PurchaseSourceDto>>(
					await _PurchaseSourceRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<PurchaseSourceDto>> GetList()
		{
			List<PurchaseSourceDto> list = _mapper.Map<List<PurchaseSourceDto>>(await _PurchaseSourceRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<PurchaseSourceDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<PurchaseSourceUpsertDto>> Get(Guid id)
		{
			PurchaseSource? entity = await _PurchaseSourceRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<PurchaseSourceUpsertDto>(Meta.NotFound());
			PurchaseSourceUpsertDto model = _mapper.Map<PurchaseSourceUpsertDto>(entity);

			return new ObjectResult<PurchaseSourceUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<PurchaseSourceUpsertDto>> Add(PurchaseSourceUpsertDto model)
		{
			if (await _PurchaseSourceRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<PurchaseSourceUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var PurchaseSource = _mapper.Map<PurchaseSource>(model);
			PurchaseSource.Id = Guid.NewGuid();

			PurchaseSource = await _PurchaseSourceRepository.AddAsync(PurchaseSource);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<PurchaseSourceUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<PurchaseSourceUpsertDto>(PurchaseSource));
		}

		public async Task<ObjectResult<PurchaseSourceUpsertDto>> Update(PurchaseSourceUpsertDto model)
		{
			if (await _PurchaseSourceRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<PurchaseSourceUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			PurchaseSource? entity = await _PurchaseSourceRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<PurchaseSourceUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _PurchaseSourceRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<PurchaseSourceUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<PurchaseSourceUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			PurchaseSource? entity = await _PurchaseSourceRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_PurchaseSourceRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}


namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class ProblemTypeService : IProblemTypeService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IProblemTypeRepository _ProblemTypeRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "ProblemType";

		public ProblemTypeService(IStringLocalizer localizer, IMapper mapper, IProblemTypeRepository ProblemTypeRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_ProblemTypeRepository = ProblemTypeRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<ProblemTypeDto>> GetAll()
		{
			return new ListResult<ProblemTypeDto>(Meta.Success(), _mapper.Map<List<ProblemTypeDto>>(
					await _ProblemTypeRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<ProblemTypeDto>> GetList()
		{
			List<ProblemTypeDto> list = _mapper.Map<List<ProblemTypeDto>>(await _ProblemTypeRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<ProblemTypeDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<ProblemTypeUpsertDto>> Get(Guid id)
		{
			ProblemType? entity = await _ProblemTypeRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<ProblemTypeUpsertDto>(Meta.NotFound());
			ProblemTypeUpsertDto model = _mapper.Map<ProblemTypeUpsertDto>(entity);

			return new ObjectResult<ProblemTypeUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<ProblemTypeUpsertDto>> Add(ProblemTypeUpsertDto model)
		{
			if (await _ProblemTypeRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<ProblemTypeUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var ProblemType = _mapper.Map<ProblemType>(model);
			ProblemType.Id = Guid.NewGuid();

			ProblemType = await _ProblemTypeRepository.AddAsync(ProblemType);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<ProblemTypeUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<ProblemTypeUpsertDto>(ProblemType));
		}

		public async Task<ObjectResult<ProblemTypeUpsertDto>> Update(ProblemTypeUpsertDto model)
		{
			if (await _ProblemTypeRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<ProblemTypeUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			ProblemType? entity = await _ProblemTypeRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<ProblemTypeUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _ProblemTypeRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<ProblemTypeUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<ProblemTypeUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			ProblemType? entity = await _ProblemTypeRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_ProblemTypeRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}

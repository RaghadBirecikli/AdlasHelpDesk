
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class TicketStatusService : ITicketStatusService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly ITicketStatusRepository _TicketStatusRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "TicketStatus";

		public TicketStatusService(IStringLocalizer localizer, IMapper mapper, ITicketStatusRepository TicketStatusRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_TicketStatusRepository = TicketStatusRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<TicketStatusDto>> GetAll()
		{
			return new ListResult<TicketStatusDto>(Meta.Success(), _mapper.Map<List<TicketStatusDto>>(
					await _TicketStatusRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<TicketStatusDto>> GetList()
		{
			List<TicketStatusDto> list = _mapper.Map<List<TicketStatusDto>>(await _TicketStatusRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<TicketStatusDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<TicketStatusUpsertDto>> Get(Guid id)
		{
			TicketStatus? entity = await _TicketStatusRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<TicketStatusUpsertDto>(Meta.NotFound());
			TicketStatusUpsertDto model = _mapper.Map<TicketStatusUpsertDto>(entity);

			return new ObjectResult<TicketStatusUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<TicketStatusUpsertDto>> Add(TicketStatusUpsertDto model)
		{
			if (await _TicketStatusRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<TicketStatusUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var TicketStatus = _mapper.Map<TicketStatus>(model);
			TicketStatus.Id = Guid.NewGuid();

			TicketStatus = await _TicketStatusRepository.AddAsync(TicketStatus);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<TicketStatusUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<TicketStatusUpsertDto>(TicketStatus));
		}

		public async Task<ObjectResult<TicketStatusUpsertDto>> Update(TicketStatusUpsertDto model)
		{
			if (await _TicketStatusRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<TicketStatusUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			TicketStatus? entity = await _TicketStatusRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<TicketStatusUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _TicketStatusRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<TicketStatusUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<TicketStatusUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			TicketStatus? entity = await _TicketStatusRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_TicketStatusRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}


namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class PublisherService : IPublisherService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IPublisherRepository _PublisherRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "Publisher";

		public PublisherService(IStringLocalizer localizer, IMapper mapper, IPublisherRepository PublisherRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_PublisherRepository = PublisherRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<PublisherDto>> GetAll()
		{
			return new ListResult<PublisherDto>(Meta.Success(), _mapper.Map<List<PublisherDto>>(
					await _PublisherRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<PublisherDto>> GetList()
		{
			List<PublisherDto> list = _mapper.Map<List<PublisherDto>>(await _PublisherRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<PublisherDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<PublisherUpsertDto>> Get(Guid id)
		{
			Publisher? entity = await _PublisherRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<PublisherUpsertDto>(Meta.NotFound());
			PublisherUpsertDto model = _mapper.Map<PublisherUpsertDto>(entity);

			return new ObjectResult<PublisherUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<PublisherUpsertDto>> Add(PublisherUpsertDto model)
		{
			if (await _PublisherRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<PublisherUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var Publisher = _mapper.Map<Publisher>(model);
			Publisher.Id = Guid.NewGuid();

			Publisher = await _PublisherRepository.AddAsync(Publisher);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<PublisherUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<PublisherUpsertDto>(Publisher));
		}

		public async Task<ObjectResult<PublisherUpsertDto>> Update(PublisherUpsertDto model)
		{
			if (await _PublisherRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<PublisherUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			Publisher? entity = await _PublisherRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<PublisherUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _PublisherRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<PublisherUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<PublisherUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			Publisher? entity = await _PublisherRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_PublisherRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}

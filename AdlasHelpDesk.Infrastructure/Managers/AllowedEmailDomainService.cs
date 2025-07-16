
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class AllowedEmailDomainService : IAllowedEmailDomainService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IAllowedEmailDomainRepository _AllowedEmailDomainRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "AllowedEmailDomain";

		public AllowedEmailDomainService(IStringLocalizer localizer,IMapper mapper, IAllowedEmailDomainRepository AllowedEmailDomainRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_AllowedEmailDomainRepository = AllowedEmailDomainRepository;
			_hostEnvironment = hostEnvironment;
			_localizer = localizer;

        }
		public async Task<ListResult<AllowedEmailDomainDto>> GetAll()
		{
			return new ListResult<AllowedEmailDomainDto>(Meta.Success(), _mapper.Map<List<AllowedEmailDomainDto>>(
					await _AllowedEmailDomainRepository.GetListAsync(null, x => x.CreatedAt, false)
                    ));
		}
		public async Task<ListResult<AllowedEmailDomainDto>> GetList()
		{
			List<AllowedEmailDomainDto> list = _mapper.Map<List<AllowedEmailDomainDto>>(await _AllowedEmailDomainRepository.GetListAsync(x => x.IsActive , x => x.CreatedAt, false));

			return new ListResult<AllowedEmailDomainDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<AllowedEmailDomainUpsertDto>> Get(Guid id)
		{
			AllowedEmailDomain? entity = await _AllowedEmailDomainRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<AllowedEmailDomainUpsertDto>(Meta.NotFound());
			AllowedEmailDomainUpsertDto model = _mapper.Map<AllowedEmailDomainUpsertDto>(entity);

			return new ObjectResult<AllowedEmailDomainUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<AllowedEmailDomainUpsertDto>> Add(AllowedEmailDomainUpsertDto model)
		{
			if (await _AllowedEmailDomainRepository.AnyAsync(x => x.DomainName == model.DomainName))
			{
				return new ObjectResult<AllowedEmailDomainUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			
			var AllowedEmailDomain = _mapper.Map<AllowedEmailDomain>(model);
			AllowedEmailDomain.Id = Guid.NewGuid();
			AllowedEmailDomain = await _AllowedEmailDomainRepository.AddAsync(AllowedEmailDomain);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<AllowedEmailDomainUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<AllowedEmailDomainUpsertDto>(AllowedEmailDomain));
		}

		public async Task<ObjectResult<AllowedEmailDomainUpsertDto>> Update(AllowedEmailDomainUpsertDto model)
		{
			

			AllowedEmailDomain? entity = await _AllowedEmailDomainRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<AllowedEmailDomainUpsertDto>(Meta.NotFound());

			entity.DomainName = model.DomainName;
			entity.IsActive = model.IsActive;
            entity.CreatedBy = model.CreatedBy ?? entity.CreatedBy;

            entity = _AllowedEmailDomainRepository.Update(entity);

			await _unitOfWork.CompleteAsync();

			return new ObjectResult<AllowedEmailDomainUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<AllowedEmailDomainUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			AllowedEmailDomain? entity = await _AllowedEmailDomainRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());

			
			_AllowedEmailDomainRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}

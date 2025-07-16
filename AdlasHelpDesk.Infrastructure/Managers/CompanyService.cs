
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class CompanyService : ICompanyService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly ICompanyRepository _CompanyRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
		private readonly string TableName = "Company";
        private readonly IStringLocalizer _localizer;

        List<string> attributes;

		public CompanyService(IStringLocalizer localizer, IUnitOfWork unitOfWork,
			ICompanyRepository CompanyRepository, IHostingEnvironment hostEnvironment, IMapper mapper, ICurrentUserService currentUserService)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_CompanyRepository = CompanyRepository;
			_hostEnvironment = hostEnvironment;
			_unitOfWork = unitOfWork;
            _localizer = localizer;
        }

		private List<string> GetTranslatedAttributes(Company member)
		{
			if (attributes == null)
			{
				attributes = new List<string>();
				attributes.Add(Functions.GetPropertyName(() => member.Description));
			}
			return attributes;
		}
		public async Task<ObjectResult<CompanyUpsertDto>> Get()
		{
			Company? entity = await _CompanyRepository.GetByIdAsync(Guid.Parse("2529f25b-9e89-43b8-97bd-a43e70d6830f"));
			if (entity is null)
				return new ObjectResult<CompanyUpsertDto>(Meta.NotFound());
			CompanyUpsertDto model = _mapper.Map<CompanyUpsertDto>(entity);

			//if (GetTranslatedAttributes(entity).Any())
			//	_CompanyRepository.GetWithTranslations(TableName, entity.Id, model, GetTranslatedAttributes(entity));

			return new ObjectResult<CompanyUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<CompanyUpsertDto>> Update(CompanyUpsertDto model)
		{
			Company? entity = await _CompanyRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<CompanyUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.Description = model.Description;
			entity.Email = model.Email;

		
			entity = _CompanyRepository.Update(entity);

			//if (GetTranslatedAttributes(entity).Any())
			//	await _CompanyRepository.UpdateTranslations(entity, model, GetTranslatedAttributes(entity));

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<CompanyUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<CompanyUpsertDto>(entity));
		}

	}
}
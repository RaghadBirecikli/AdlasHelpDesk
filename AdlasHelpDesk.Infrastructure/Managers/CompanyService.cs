
using System.Globalization;

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

		List<string> attributes;

		public CompanyService(IUnitOfWork unitOfWork,
			ICompanyRepository CompanyRepository, IHostingEnvironment hostEnvironment, IMapper mapper, ICurrentUserService currentUserService)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_CompanyRepository = CompanyRepository;
			_hostEnvironment = hostEnvironment;
			_unitOfWork = unitOfWork;
		}

		private List<string> GetTranslatedAttributes(Company member)
		{
			if (attributes == null)
			{
				attributes = new List<string>();
				attributes.Add(Functions.GetPropertyName(() => member.Description));
				attributes.Add(Functions.GetPropertyName(() => member.FooterDescription));
				attributes.Add(Functions.GetPropertyName(() => member.SoftwareProductPageDescription));
				attributes.Add(Functions.GetPropertyName(() => member.AboutUsPageDescription));
				attributes.Add(Functions.GetPropertyName(() => member.AboutUsPageContext));
				attributes.Add(Functions.GetPropertyName(() => member.ContactPageDescription));
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
			entity.FirstPhone = model.FirstPhone;
			entity.SecondPhone = model.SecondPhone;
			entity.GooglePlus = model.GooglePlus;
			entity.YouTube = model.YouTube;
			entity.FirstAddress = model.FirstAddress;
			entity.SecondAddress = model.SecondAddress;
			entity.Description = model.Description;
			entity.Email = model.Email;
			entity.Twiter = model.Twiter;
			entity.FaceBook = model.FaceBook;
			entity.FooterDescription = model.FooterDescription;
			entity.AboutUsPageDescription = model.AboutUsPageDescription;
			entity.SoftwareProductPageDescription = model.SoftwareProductPageDescription;
			entity.ContactPageDescription = model.ContactPageDescription;
			entity.Instagram = model.Instagram;
			entity.Linkedin = model.Linkedin;
			entity.AboutUsPageContext = model.AboutUsPageContext;


			if (model.IconFile != null)
			{
				if (!string.IsNullOrEmpty(entity.Icon))
					Functions.DeleteImage(entity.Icon, entity, _hostEnvironment);

				string imagePath = await Functions.SaveImage(model.IconFile, entity, _hostEnvironment);
				entity.Icon = imagePath;
			}
			if (model.LogoFile != null)
			{
				if (!string.IsNullOrEmpty(entity.Logo))
					Functions.DeleteImage(entity.Logo, entity, _hostEnvironment);

				string imagePath = await Functions.SaveImage(model.LogoFile, entity, _hostEnvironment);
				entity.Logo = imagePath;
			}
			entity = _CompanyRepository.Update(entity);

			//if (GetTranslatedAttributes(entity).Any())
			//	await _CompanyRepository.UpdateTranslations(entity, model, GetTranslatedAttributes(entity));

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<CompanyUpsertDto>(Meta.CustomSuccess(ConstantMessages.RecordUpdated), _mapper.Map<CompanyUpsertDto>(entity));
		}

	}
}

namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class MemberService : IMemberService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMemberRepository _memberRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
		List<string> attributes;
		private readonly string TableName = "Member";
        private readonly IStringLocalizer _localizer;

        public MemberService(IStringLocalizer localizer, IMapper mapper, IMemberRepository memberRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_memberRepository = memberRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		private List<string> GetTranslatedAttributes(Member member)
		{
			if (attributes == null)
			{
				attributes = new List<string>();
				attributes.Add(Functions.GetPropertyName(() => member.JobTitle));
			}
			return attributes;
		}
		public async Task<ListResult<MemberDto>> GetAll()
		{
			return new ListResult<MemberDto>(Meta.Success(), _mapper.Map<List<MemberDto>>(
					await _memberRepository.GetListAsync(null, x => x.FullName, false)
					));
		}
		public async Task<ListResult<MemberDto>> GetList()
		{
			List<MemberDto> list = _mapper.Map<List<MemberDto>>(await _memberRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			var translatedAttributes = GetTranslatedAttributes(new Member());
			//if (translatedAttributes.Any())
			//	foreach (MemberDto model in list)
			//	{
			//		_memberRepository.GetWithTranslations(TableName, model.Id, model, translatedAttributes);
			//	}
			return new ListResult<MemberDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<MemberUpsertDto>> Get(Guid id)
		{
			Member? entity = await _memberRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<MemberUpsertDto>(Meta.NotFound());
			MemberUpsertDto model = _mapper.Map<MemberUpsertDto>(entity);

			//if (GetTranslatedAttributes(entity).Any())
			//	_memberRepository.GetWithTranslations(TableName, entity.Id, model, GetTranslatedAttributes(entity));

			return new ObjectResult<MemberUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<MemberUpsertDto>> Add(MemberUpsertDto model)
		{
			if (await _memberRepository.AnyAsync(x => x.Name == model.Name && x.Surname == model.Surname))
			{
				return new ObjectResult<MemberUpsertDto>(Meta.CustomError("Ad ve Soyad başka bir kullanıcıya ait!"));
			}
			else if (await _memberRepository.AnyAsync(x => x.UserName == model.UserName))
			{
				return new ObjectResult<MemberUpsertDto>(Meta.CustomError("Kullanıcı adı başka bir kullanıcıya ait!"));
			}
			var member = _mapper.Map<Member>(model);
			member.Password = Functions.MD5(model.Password);
			member.Id = Guid.NewGuid();

		

			member = await _memberRepository.AddAsync(member);

			//if (GetTranslatedAttributes(member).Any())
			//	await _memberRepository.AddTranlations(member, model, GetTranslatedAttributes(member));

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<MemberUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<MemberUpsertDto>(member));
		}

		public async Task<ObjectResult<MemberUpsertDto>> Update(MemberUpsertDto model)
		{
			if (await _memberRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name && x.Surname == model.Surname))
			{
				return new ObjectResult<MemberUpsertDto>(Meta.CustomError("Ad ve Soyad başka bir kullanıcıya ait!"));
			}
			else if (await _memberRepository.AnyAsync(x => x.Id != model.Id && x.UserName == model.UserName))
			{
				return new ObjectResult<MemberUpsertDto>(Meta.CustomError("Kullanıcı adı başka bir kullanıcıya ait!"));
			}

			Member? entity = await _memberRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<MemberUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.Surname = model.Surname;
			entity.UserName = model.UserName;
			entity.IsActive = model.IsActive;
			entity.IsAdmin = model.IsAdmin;
			entity.Email = model.Email;
			entity.JobTitle = model.JobTitle;
			entity.Phone = model.Phone;
			entity.Description = model.Description;
			entity.Address = model.Address;

		
			if (model.Password != null)
				entity.Password = Functions.MD5(model.Password);
			entity = _memberRepository.Update(entity);

			//if (GetTranslatedAttributes(entity).Any())
			//	await _memberRepository.UpdateTranslations(entity, model, GetTranslatedAttributes(entity));
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<MemberUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<MemberUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			Member? entity = await _memberRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());

			if (_memberRepository.GetListAsync().Result.Count() == 1)
			{
				return new Result(Meta.CustomError("Son Personel Silinmez!"));
			}

			
			_memberRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		public async Task<PagingResult<MemberDto>> GetPage(MemberPageFilter filter)
		{
			var pre = ExpressionBuilder.New<Member>(true);
			if (filter.PublicFilter != null)
			{
				filter.PublicFilter = filter.PublicFilter.Replace('i', 'İ').Replace('ı', 'I').ToUpper();
				pre.And(x => x.FullName.ToUpper().Contains(filter.PublicFilter)
				|| (x.Email != null && x.Email.ToUpper().Contains(filter.PublicFilter)));
			}
			if (filter.isActive != null) pre.And(x => x.IsActive == filter.isActive);
			PagingResult<Member> Members = await _memberRepository.GetPage(new PagingResult<Member>
			{
				Meta = filter.PagingResult.Meta,
				PagingParameters = filter.PagingResult.PagingParameters,
				Entities = _mapper.Map<List<Member>>(filter.PagingResult.Entities)
			}, pre);
			filter.PagingResult.Meta = Members.Meta;
			filter.PagingResult.PagingParameters = Members.PagingParameters;
			filter.PagingResult.Entities = _mapper.Map<List<MemberDto>>(Members.Entities);
			return filter.PagingResult;
		}

	}
}

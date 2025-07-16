
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class LibraryService : ILibraryService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly ILibraryRepository _LibraryRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
		private readonly string TableName = "Library";

		public LibraryService(IStringLocalizer localizer, IMapper mapper, ILibraryRepository LibraryRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_LibraryRepository = LibraryRepository;
			_hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

		
		public async Task<ListResult<LibraryDto>> GetAll()
		{
			return new ListResult<LibraryDto>(Meta.Success(), _mapper.Map<List<LibraryDto>>(
					await _LibraryRepository.GetListAsync(null, x => x.Name, false)
					));
		}
		public async Task<ListResult<LibraryDto>> GetList()
		{
			List<LibraryDto> list = _mapper.Map<List<LibraryDto>>(await _LibraryRepository.GetListAsync(x => x.IsActive , x => x.Name, false));

			return new ListResult<LibraryDto>(Meta.Success(), list);
		}
		public async Task<ObjectResult<LibraryUpsertDto>> Get(Guid id)
		{
			Library? entity = await _LibraryRepository.GetByIdAsync(id);
			if (entity is null)
				return new ObjectResult<LibraryUpsertDto>(Meta.NotFound());
			LibraryUpsertDto model = _mapper.Map<LibraryUpsertDto>(entity);

			return new ObjectResult<LibraryUpsertDto>(Meta.Success(), model);
		}

		public async Task<ObjectResult<LibraryUpsertDto>> Add(LibraryUpsertDto model)
		{
			if (await _LibraryRepository.AnyAsync(x => x.Name == model.Name))
			{
				return new ObjectResult<LibraryUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			var Library = _mapper.Map<Library>(model);
			Library.Id = Guid.NewGuid();

			Library = await _LibraryRepository.AddAsync(Library);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<LibraryUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<LibraryUpsertDto>(Library));
		}

		public async Task<ObjectResult<LibraryUpsertDto>> Update(LibraryUpsertDto model)
		{
			if (await _LibraryRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name ))
			{
				return new ObjectResult<LibraryUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
			}
			

			Library? entity = await _LibraryRepository.GetByIdAsync(model.Id.Value);
			if (entity is null)
				return new ObjectResult<LibraryUpsertDto>(Meta.NotFound());

			entity.Name = model.Name;
			entity.IsActive = model.IsActive;
			
			entity = _LibraryRepository.Update(entity);

			
			await _unitOfWork.CompleteAsync();

			return new ObjectResult<LibraryUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<LibraryUpsertDto>(entity));
		}
		public async Task<Result> Delete(Guid id)
		{
			Library? entity = await _LibraryRepository.GetByIdAsync(id);
			if (entity is null)
				return new Result(Meta.NotFound());


			_LibraryRepository.Delete(entity);
			await _unitOfWork.CompleteAsync();
			return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
		}
		

	}
}

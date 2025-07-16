
namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class UniversityService : IUniversityService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUniversityRepository _UniversityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        List<string> attributes;
        private readonly string TableName = "University";

        public UniversityService(IStringLocalizer localizer, IMapper mapper, IUniversityRepository UniversityRepository, IHostingEnvironment hostEnvironment, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _UniversityRepository = UniversityRepository;
            _hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }


        public async Task<ListResult<UniversityDto>> GetAll(UniversityFilter? filter)
        {

            if (filter.PublicFilter != null && filter.isActive != null)
            {
                var keyword = filter.PublicFilter.Trim();

                return new ListResult<UniversityDto>(
                    Meta.Success(),
                    _mapper.Map<List<UniversityDto>>(
                        await _UniversityRepository.GetListAsync(
                            x => x.Name.Contains(keyword)&&x.IsActive==filter.isActive,
                            x => x.Name,
                            false
                        )
                    )
                );
            }
            else if (!string.IsNullOrEmpty(filter.PublicFilter) && filter.isActive == null)
            {

                var keyword = filter.PublicFilter.Trim();

                return new ListResult<UniversityDto>(
                    Meta.Success(),
                    _mapper.Map<List<UniversityDto>>(
                        await _UniversityRepository.GetListAsync(
                            x => x.Name.Contains(keyword),
                            x => x.Name,
                            false
                        )
                    )
                );
            }
            else if (filter.PublicFilter == null && filter.isActive != null)
            {

                return new ListResult<UniversityDto>(Meta.Success(), _mapper.Map<List<UniversityDto>>(
                    await _UniversityRepository.GetListAsync(x => x.IsActive == filter.isActive, x => x.Name, false)
                    ));
            }
            else
                return new ListResult<UniversityDto>(Meta.Success(), _mapper.Map<List<UniversityDto>>(
                        await _UniversityRepository.GetListAsync(null, x => x.Name, false)
                        ));
        }
        public async Task<ListResult<UniversityDto>> GetList()
        {
            List<UniversityDto> list = _mapper.Map<List<UniversityDto>>(await _UniversityRepository.GetListAsync(x => x.IsActive, x => x.Name, false));

            return new ListResult<UniversityDto>(Meta.Success(), list);
        }
        public async Task<ObjectResult<UniversityUpsertDto>> Get(Guid id)
        {
            University? entity = await _UniversityRepository.GetByIdAsync(id);
            if (entity is null)
                return new ObjectResult<UniversityUpsertDto>(Meta.NotFound());
            UniversityUpsertDto model = _mapper.Map<UniversityUpsertDto>(entity);

            return new ObjectResult<UniversityUpsertDto>(Meta.Success(), model);
        }

        public async Task<ObjectResult<UniversityUpsertDto>> Add(UniversityUpsertDto model)
        {
            if (await _UniversityRepository.AnyAsync(x => x.Name == model.Name))
            {
                return new ObjectResult<UniversityUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
            }
            var University = _mapper.Map<University>(model);
            University.Id = Guid.NewGuid();

            University = await _UniversityRepository.AddAsync(University);

            await _unitOfWork.CompleteAsync();
            return new ObjectResult<UniversityUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<UniversityUpsertDto>(University));
        }

        public async Task<ObjectResult<UniversityUpsertDto>> Update(UniversityUpsertDto model)
        {
            if (await _UniversityRepository.AnyAsync(x => x.Id != model.Id && x.Name == model.Name))
            {
                return new ObjectResult<UniversityUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
            }


            University? entity = await _UniversityRepository.GetByIdAsync(model.Id.Value);
            if (entity is null)
                return new ObjectResult<UniversityUpsertDto>(Meta.NotFound());

            entity.Name = model.Name;
            entity.IsActive = model.IsActive;

            entity = _UniversityRepository.Update(entity);


            await _unitOfWork.CompleteAsync();

            return new ObjectResult<UniversityUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<UniversityUpsertDto>(entity));
        }
        public async Task<Result> Delete(Guid id)
        {
            University? entity = await _UniversityRepository.GetByIdAsync(id);
            if (entity is null)
                return new Result(Meta.NotFound());


            _UniversityRepository.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
        }


    }
}


namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class SkillService : ISkillService
    {
        private readonly IMapper _mapper;
        private readonly ISkillRepository _skillRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public SkillService(
            IMapper mapper,
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer localizer)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<ListResult<SkillDto>> GetAll()
        {
            var list = await _skillRepository.GetListAsync(null, x => x.Name, false);
            return new ListResult<SkillDto>(Meta.Success(), _mapper.Map<List<SkillDto>>(list));
        }
        public async Task<ListResult<SkillDto>> GetSkillsByPublisher(Guid publisherId)
        {
            var list = await _skillRepository.GetListAsync(x=>x.PublisherId==publisherId, x => x.Name, false);
            return new ListResult<SkillDto>(Meta.Success(), _mapper.Map<List<SkillDto>>(list));
        }

        public async Task<ListResult<SkillDto>> GetList()
        {
            var list = await _skillRepository.GetListAsync(x => x.IsActive, x => x.Name, false);
            return new ListResult<SkillDto>(Meta.Success(), _mapper.Map<List<SkillDto>>(list));
        }

        public async Task<ObjectResult<SkillUpsertDto>> Get(Guid id)
        {
            var entity = await _skillRepository.GetByIdAsync(id);
            if (entity == null)
                return new ObjectResult<SkillUpsertDto>(Meta.NotFound());

            return new ObjectResult<SkillUpsertDto>(Meta.Success(), _mapper.Map<SkillUpsertDto>(entity));
        }

        public async Task<ObjectResult<SkillUpsertDto>> Add(SkillUpsertDto model)
        {
            if (await _skillRepository.AnyAsync(x => x.Name == model.Name && x.PublisherId == model.PublisherId))
            {
                return new ObjectResult<SkillUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
            }

            var entity = _mapper.Map<Skill>(model);
            entity.Id = Guid.NewGuid();

            await _skillRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<SkillUpsertDto>(
                Meta.CustomSuccess(_localizer["RecordAdded"]),
                _mapper.Map<SkillUpsertDto>(entity));
        }

        public async Task<ObjectResult<SkillUpsertDto>> Update(SkillUpsertDto model)
        {
            var entity = await _skillRepository.GetByIdAsync(model.Id.Value);
            if (entity == null)
                return new ObjectResult<SkillUpsertDto>(Meta.NotFound());

            if (await _skillRepository.AnyAsync(x =>
                    x.Id != model.Id &&
                    x.Name == model.Name &&
                    x.PublisherId == model.PublisherId))
            {
                return new ObjectResult<SkillUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
            }

            entity.Name = model.Name;
            entity.IsActive = model.IsActive;
            entity.PublisherId = model.PublisherId;

            _skillRepository.Update(entity);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<SkillUpsertDto>(
                Meta.CustomSuccess(_localizer["RecordUpdated"]),
                _mapper.Map<SkillUpsertDto>(entity));
        }

        public async Task<Result> Delete(Guid id)
        {
            var entity = await _skillRepository.GetByIdAsync(id);
            if (entity == null)
                return new Result(Meta.NotFound());

            _skillRepository.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
        }
    }
}

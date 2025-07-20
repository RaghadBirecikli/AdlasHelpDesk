
namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class ProductNameService : IProductNameService
    {
        private readonly IMapper _mapper;
        private readonly IProductNameRepository _productNameRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public ProductNameService(
            IMapper mapper,
            IProductNameRepository productNameRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer localizer)
        {
            _mapper = mapper;
            _productNameRepository = productNameRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<ListResult<ProductNameDto>> GetAll()
        {
            var list = await _productNameRepository.GetListAsync(null, x => x.Name, false);
            return new ListResult<ProductNameDto>(Meta.Success(), _mapper.Map<List<ProductNameDto>>(list));
        }
        public async Task<ListResult<ProductNameDto>> GetProductNamesByPublisher(Guid publisherId)
        {
            var list = await _productNameRepository.GetListAsync(x=>x.PublisherId==publisherId, x => x.Name, false);
            return new ListResult<ProductNameDto>(Meta.Success(), _mapper.Map<List<ProductNameDto>>(list));
        }

        public async Task<ListResult<ProductNameDto>> GetList()
        {
            var list = await _productNameRepository.GetListAsync(x => x.IsActive, x => x.Name, false);
            return new ListResult<ProductNameDto>(Meta.Success(), _mapper.Map<List<ProductNameDto>>(list));
        }

        public async Task<ObjectResult<ProductNameUpsertDto>> Get(Guid id)
        {
            var entity = await _productNameRepository.GetByIdAsync(id);
            if (entity == null)
                return new ObjectResult<ProductNameUpsertDto>(Meta.NotFound());

            return new ObjectResult<ProductNameUpsertDto>(Meta.Success(), _mapper.Map<ProductNameUpsertDto>(entity));
        }

        public async Task<ObjectResult<ProductNameUpsertDto>> Add(ProductNameUpsertDto model)
        {
            if (await _productNameRepository.AnyAsync(x => x.Name == model.Name && x.PublisherId == model.PublisherId))
            {
                return new ObjectResult<ProductNameUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
            }

            var entity = _mapper.Map<ProductName>(model);
            entity.Id = Guid.NewGuid();

            await _productNameRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<ProductNameUpsertDto>(
                Meta.CustomSuccess(_localizer["RecordAdded"]),
                _mapper.Map<ProductNameUpsertDto>(entity));
        }

        public async Task<ObjectResult<ProductNameUpsertDto>> Update(ProductNameUpsertDto model)
        {
            var entity = await _productNameRepository.GetByIdAsync(model.Id.Value);
            if (entity == null)
                return new ObjectResult<ProductNameUpsertDto>(Meta.NotFound());

            if (await _productNameRepository.AnyAsync(x =>
                    x.Id != model.Id &&
                    x.Name == model.Name &&
                    x.PublisherId == model.PublisherId))
            {
                return new ObjectResult<ProductNameUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));
            }

            entity.Name = model.Name;
            entity.IsActive = model.IsActive;
            entity.PublisherId = model.PublisherId;

            _productNameRepository.Update(entity);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<ProductNameUpsertDto>(
                Meta.CustomSuccess(_localizer["RecordUpdated"]),
                _mapper.Map<ProductNameUpsertDto>(entity));
        }

        public async Task<Result> Delete(Guid id)
        {
            var entity = await _productNameRepository.GetByIdAsync(id);
            if (entity == null)
                return new Result(Meta.NotFound());

            _productNameRepository.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
        }
    }
}

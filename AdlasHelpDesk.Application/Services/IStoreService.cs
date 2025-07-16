
namespace AdlasHelpDesk.Application.Services
{
    public interface IStoreService
    {
        Task<ObjectResult<StoreUpsertDto>> Get(Guid id);
        Task<ListResult<StoreDto>> GetAll();
        Task<ListResult<StoreDto>> GetList();
        Task<ObjectResult<StoreUpsertDto>> Add(StoreUpsertDto model);
        Task<ObjectResult<StoreUpsertDto>> Update(StoreUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

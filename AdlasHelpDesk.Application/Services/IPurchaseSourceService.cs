
namespace AdlasHelpDesk.Application.Services
{
    public interface IPurchaseSourceService
    {
        Task<ObjectResult<PurchaseSourceUpsertDto>> Get(Guid id);
        Task<ListResult<PurchaseSourceDto>> GetAll();
        Task<ListResult<PurchaseSourceDto>> GetList();
        Task<ObjectResult<PurchaseSourceUpsertDto>> Add(PurchaseSourceUpsertDto model);
        Task<ObjectResult<PurchaseSourceUpsertDto>> Update(PurchaseSourceUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

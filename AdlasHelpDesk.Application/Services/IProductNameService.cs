
using AdlasHelpDesk.Domain.Models;

namespace AdlasHelpDesk.Application.Services
{
    public interface IProductNameService
    {
        Task<ObjectResult<ProductNameUpsertDto>> Get(Guid id);
        Task<ListResult<ProductNameDto>> GetAll();
        Task<ListResult<ProductNameDto>> GetProductNamesByPublisher(Guid publisherId);
        Task<ListResult<ProductNameDto>> GetList();
        Task<ObjectResult<ProductNameUpsertDto>> Add(ProductNameUpsertDto model);
        Task<ObjectResult<ProductNameUpsertDto>> Update(ProductNameUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

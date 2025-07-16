
namespace AdlasHelpDesk.Application.Services
{
    public interface IProductService
    {
        Task<ObjectResult<ProductUpsertDto>> Get(Guid id);
        Task<ListResult<ProductDto>> GetAll();
        Task<ListResult<ProductDto>> GetList();
        Task<ObjectResult<ProductUpsertDto>> Add(ProductUpsertDto model);
        Task<ObjectResult<ProductUpsertDto>> Update(ProductUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

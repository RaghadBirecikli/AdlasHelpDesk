
namespace AdlasHelpDesk.Application.Services
{
    public interface IProductTypeService
    {
        Task<ObjectResult<ProductTypeUpsertDto>> Get(Guid id);
        Task<ListResult<ProductTypeDto>> GetAll();
        Task<ListResult<ProductTypeDto>> GetList();
        Task<ObjectResult<ProductTypeUpsertDto>> Add(ProductTypeUpsertDto model);
        Task<ObjectResult<ProductTypeUpsertDto>> Update(ProductTypeUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

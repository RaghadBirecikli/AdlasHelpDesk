
namespace AdlasHelpDesk.Application.Services
{
    public interface IPublisherService
    {
        Task<ObjectResult<PublisherUpsertDto>> Get(Guid id);
        Task<ListResult<PublisherDto>> GetAll();
        Task<ListResult<PublisherDto>> GetList();
        Task<ObjectResult<PublisherUpsertDto>> Add(PublisherUpsertDto model);
        Task<ObjectResult<PublisherUpsertDto>> Update(PublisherUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

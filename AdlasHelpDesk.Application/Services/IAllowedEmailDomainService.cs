
namespace AdlasHelpDesk.Application.Services
{
    public interface IAllowedEmailDomainService
    {
        Task<ObjectResult<AllowedEmailDomainUpsertDto>> Get(Guid id);
        Task<ListResult<AllowedEmailDomainDto>> GetAll();
        Task<ListResult<AllowedEmailDomainDto>> GetList();
        Task<ObjectResult<AllowedEmailDomainUpsertDto>> Add(AllowedEmailDomainUpsertDto model);
        Task<ObjectResult<AllowedEmailDomainUpsertDto>> Update(AllowedEmailDomainUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

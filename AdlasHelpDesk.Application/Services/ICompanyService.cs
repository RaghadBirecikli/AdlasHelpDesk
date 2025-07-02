
namespace AdlasHelpDesk.Application.Services
{
    public interface ICompanyService 
    {
        Task<ObjectResult<CompanyUpsertDto>> Get();
        Task<ObjectResult<CompanyUpsertDto>> Update(CompanyUpsertDto model);
    }
}
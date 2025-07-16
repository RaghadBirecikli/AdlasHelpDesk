
namespace AdlasHelpDesk.Application.Services
{
    public interface IUniversityService
    {
        Task<ObjectResult<UniversityUpsertDto>> Get(Guid id);
        Task<ListResult<UniversityDto>> GetAll(UniversityFilter? filter);
        Task<ListResult<UniversityDto>> GetList();
        Task<ObjectResult<UniversityUpsertDto>> Add(UniversityUpsertDto model);
        Task<ObjectResult<UniversityUpsertDto>> Update(UniversityUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

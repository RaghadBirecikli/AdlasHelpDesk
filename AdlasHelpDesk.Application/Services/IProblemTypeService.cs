
namespace AdlasHelpDesk.Application.Services
{
    public interface IProblemTypeService
    {
        Task<ObjectResult<ProblemTypeUpsertDto>> Get(Guid id);
        Task<ListResult<ProblemTypeDto>> GetAll();
        Task<ListResult<ProblemTypeDto>> GetList();
        Task<ObjectResult<ProblemTypeUpsertDto>> Add(ProblemTypeUpsertDto model);
        Task<ObjectResult<ProblemTypeUpsertDto>> Update(ProblemTypeUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

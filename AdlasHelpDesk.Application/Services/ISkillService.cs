
using AdlasHelpDesk.Domain.Models;

namespace AdlasHelpDesk.Application.Services
{
    public interface ISkillService
    {
        Task<ObjectResult<SkillUpsertDto>> Get(Guid id);
        Task<ListResult<SkillDto>> GetAll();
        Task<ListResult<SkillDto>> GetSkillsByPublisher(Guid publisherId);
        Task<ListResult<SkillDto>> GetList();
        Task<ObjectResult<SkillUpsertDto>> Add(SkillUpsertDto model);
        Task<ObjectResult<SkillUpsertDto>> Update(SkillUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

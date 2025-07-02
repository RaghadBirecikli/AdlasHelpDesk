
namespace AdlasHelpDesk.Application.Services
{
    public interface IMemberService
    {
        Task<ObjectResult<MemberUpsertDto>> Get(Guid id);
        Task<ListResult<MemberDto>> GetAll();
        Task<ListResult<MemberDto>> GetList();
        Task<ObjectResult<MemberUpsertDto>> Add(MemberUpsertDto model);
        Task<ObjectResult<MemberUpsertDto>> Update(MemberUpsertDto model);
        Task<Result> Delete(Guid id);
        Task<PagingResult<MemberDto>> GetPage(MemberPageFilter filter);

    }
}

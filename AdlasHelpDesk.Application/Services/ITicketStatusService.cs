
namespace AdlasHelpDesk.Application.Services
{
    public interface ITicketStatusService
    {
        Task<ObjectResult<TicketStatusUpsertDto>> Get(Guid id);
        Task<ListResult<TicketStatusDto>> GetAll();
        Task<ListResult<TicketStatusDto>> GetList();
        Task<ObjectResult<TicketStatusUpsertDto>> Add(TicketStatusUpsertDto model);
        Task<ObjectResult<TicketStatusUpsertDto>> Update(TicketStatusUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

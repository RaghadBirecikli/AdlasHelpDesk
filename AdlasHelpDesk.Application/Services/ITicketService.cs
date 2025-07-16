
namespace AdlasHelpDesk.Application.Services
{
    public interface ITicketService
    {
        Task<ObjectResult<TicketUpsertDto>> Get(Guid id);
        Task<ListResult<TicketDto>> GetAll();
        Task<ObjectResult<TicketUpsertDto>> Add(TicketUpsertDto model);
        Task<ObjectResult<TicketUpsertDto>> Update(TicketUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

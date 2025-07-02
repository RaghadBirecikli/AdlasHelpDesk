
namespace AdlasHelpDesk.Application.Services
{
    public interface ISendMailService 
    {
        Task<ObjectResult<SendMailUpsertDto>> Add(SendMailUpsertDto model);
        
    }
}
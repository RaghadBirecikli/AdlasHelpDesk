
namespace AdlasHelpDesk.Application.Services
{
    public interface ICustomerService
    {
        Task<ObjectResult<CustomerDto>> Get(Guid id);
        Task<ListResult<CustomerDto>> GetAll();
        Task<ObjectResult<CustomerUpsertDto>> Add(CustomerUpsertDto model);
        Task<ObjectResult<CustomerUpsertDto>> Update(CustomerUpsertDto model);
        Task<Result> VerifyEmail(string email, string verificationCode);

        Task<Result> Delete(Guid id);

    }
}

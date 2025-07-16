
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class CustomerService : ICustomerService
	{
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;
        private readonly ISendMailService _sendMailService;
        List<string> attributes;
		private readonly string TableName = "Customer";

		public CustomerService(IStringLocalizer localizer,IMapper mapper, ICustomerRepository CustomerRepository, IHostingEnvironment hostEnvironment,  ICurrentUserService currentUserService, IUnitOfWork unitOfWork, ISendMailService sendMailService)
		{
			_mapper = mapper;
			_currentUserService = currentUserService;
			_unitOfWork = unitOfWork;
			_customerRepository = CustomerRepository;
			_hostEnvironment = hostEnvironment;
			_localizer = localizer;
            _sendMailService = sendMailService;

        }
        public async Task<ListResult<CustomerDto>> GetAll()
        {
            var customers = await _customerRepository.GetListAsync();
            return new ListResult<CustomerDto>(Meta.Success(), _mapper.Map<List<CustomerDto>>(customers));
        }

        public async Task<ObjectResult<CustomerDto>> Get(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return new ObjectResult<CustomerDto>(Meta.NotFound());

            return new ObjectResult<CustomerDto>(Meta.Success(), _mapper.Map<CustomerDto>(customer));
        }

        public async Task<ObjectResult<CustomerUpsertDto>> Add(CustomerUpsertDto model)
        {
            if (await _customerRepository.AnyAsync(c => c.Email == model.Email))
                return new ObjectResult<CustomerUpsertDto>(Meta.CustomError(_localizer["RecordAlreadyExists"]));

            var entity = _mapper.Map<Customer>(model);
            entity.Id = Guid.NewGuid();
            entity.VerificationCode = GenerateVerificationCode();

            await _customerRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            // TODO: Send email with entity.VerificationCode
          

            return new ObjectResult<CustomerUpsertDto>(Meta.CustomSuccess(_localizer["RecordAdded"]), _mapper.Map<CustomerUpsertDto>(entity));
        }

        public async Task<ObjectResult<CustomerUpsertDto>> Update(CustomerUpsertDto model)
        {
            var existing = await _customerRepository.GetByIdAsync(model.Id.Value);
            if (existing == null)
                return new ObjectResult<CustomerUpsertDto>(Meta.NotFound());

            existing.Name = model.Name;
            existing.Email = model.Email;
            existing.UniversityNumber = model.UniversityNumber;
            existing.UniversityId = model.UniversityId;

            _customerRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<CustomerUpsertDto>(Meta.CustomSuccess(_localizer["RecordUpdated"]), _mapper.Map<CustomerUpsertDto>(existing));
        }

        public async Task<Result> Delete(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return new Result(Meta.NotFound());

            _customerRepository.Delete(customer);
            await _unitOfWork.CompleteAsync();

            return new Result(Meta.CustomSuccess(_localizer["RecordDeleted"]));
        }

        public async Task<Result> VerifyEmail(string email, string verificationCode)
        {
            var customer = await _customerRepository.GetAsync(c => c.Email == email && c.VerificationCode == verificationCode);
            if (customer == null)
                return new Result(Meta.CustomError(_localizer["VerificationFailed"] ?? "Verification failed."));

            customer.IsEmailVerified = true;
            customer.VerificationCode = null;

            _customerRepository.Update(customer);
            await _unitOfWork.CompleteAsync();

            return new Result(Meta.CustomSuccess(_localizer["EmailVerified"] ?? "Email verified successfully."));
        }

        private string GenerateVerificationCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}

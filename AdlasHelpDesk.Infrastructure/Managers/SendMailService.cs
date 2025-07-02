
namespace AdlasHelpDesk.Infrastructure.Managers
{
	public class SendMailService : ISendMailService
	{
		private readonly IMapper _mapper;
		private readonly ISendMailRepository _SendMailRepository;
		private readonly IUnitOfWork _unitOfWork;
		List<string> attributes;
		private readonly string TableName = "SendMail";

		public SendMailService(IUnitOfWork unitOfWork,
			ISendMailRepository SendMailRepository, IMapper mapper)
		{
			_mapper = mapper;
			_SendMailRepository = SendMailRepository;
			_unitOfWork = unitOfWork;
		}
		

		public async Task<ObjectResult<SendMailUpsertDto>> Add(SendMailUpsertDto model)
		{
			
			var SendMail = _mapper.Map<SendMail>(model);
			SendMail.Id = Guid.NewGuid();
			
			SendMail = await _SendMailRepository.AddAsync(SendMail);

			await _unitOfWork.CompleteAsync();
			return new ObjectResult<SendMailUpsertDto>(Meta.CustomSuccess(ConstantMessages.RecordAdded), _mapper.Map<SendMailUpsertDto>(SendMail));
		}


	}
}
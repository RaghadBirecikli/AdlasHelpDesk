

namespace AdlasHelpDesk.Infrastructure.Managers
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IStringLocalizer _localizer;

        public TicketService(
            ITicketRepository ticketRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IHostingEnvironment hostEnvironment,
            IStringLocalizer localizer)
        {
            _ticketRepository = ticketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _hostEnvironment = hostEnvironment;
            _localizer = localizer;
        }

        public async Task<ObjectResult<TicketUpsertDto>> Add(TicketUpsertDto model)
        {
            var ticket = _mapper.Map<Ticket>(model);
            ticket.Id = Guid.NewGuid();
            ticket.TicketNumber = await GenerateTicketNumber();
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.AllDataConfirmed = false;

            await _ticketRepository.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<TicketUpsertDto>(
                Meta.CustomSuccess("Ticket added successfully"),
                _mapper.Map<TicketUpsertDto>(ticket)
            );
        }

        public async Task<ObjectResult<TicketUpsertDto>> Get(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return new ObjectResult<TicketUpsertDto>(Meta.NotFound());

            return new ObjectResult<TicketUpsertDto>(
                Meta.Success(),
                _mapper.Map<TicketUpsertDto>(ticket)
            );
        }

        public async Task<ListResult<TicketDto>> GetAll()
        {
            var list = await _ticketRepository.GetListAsync(null, x => x.CreatedAt, false);
            return new ListResult<TicketDto>(Meta.Success(), _mapper.Map<List<TicketDto>>(list));
        }
        public async Task<ObjectResult<TicketUpsertDto>> Update(TicketUpsertDto model)
        {
            var entity = await _ticketRepository.GetByIdAsync(model.Id.Value);
            if (entity == null)
                return new ObjectResult<TicketUpsertDto>(Meta.NotFound());

            // Update all properties
            entity.CustomerId = model.CustomerId;
            entity.ProductId = model.ProductId;
            entity.ProblemTypeId = model.ProblemTypeId;
            entity.PurchaseSourceId = model.PurchaseSourceId;
            entity.StoreNameId = model.StoreNameId;
            entity.LibraryNameId = model.LibraryNameId;
            entity.OrderNumber = model.OrderNumber;
            entity.InvoiceImageUrl = model.InvoiceImageUrl;
            entity.ProblemDescription = model.ProblemDescription;
            entity.ProblemImageUrl = model.ProblemImageUrl;
            entity.CodesNumber = model.CodesNumber;
            entity.PurchaseDataConfirmed = model.PurchaseDataConfirmed;
            entity.AllDataConfirmed = model.AllDataConfirmed;
            entity.TicketStatusId = model.TicketStatusId;

            _ticketRepository.Update(entity);
            await _unitOfWork.CompleteAsync();

            return new ObjectResult<TicketUpsertDto>(
                Meta.CustomSuccess("Ticket updated successfully"),
                _mapper.Map<TicketUpsertDto>(entity)
            );
        }

        public async Task<Result> Delete(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return new Result(Meta.NotFound());

            _ticketRepository.Delete(ticket);
            await _unitOfWork.CompleteAsync();

            return new Result(Meta.CustomSuccess("Ticket deleted"));
        }

        private async Task<string> GenerateTicketNumber()
        {
            var today = DateTime.UtcNow.ToString("yyyyMMdd");
            var existingCount = await _ticketRepository.CountAsync(x => x.CreatedAt.Date == DateTime.UtcNow.Date);
            var sequence = existingCount + 1;
            return $"HD-{today}-{sequence:D3}";
        }
    }
}

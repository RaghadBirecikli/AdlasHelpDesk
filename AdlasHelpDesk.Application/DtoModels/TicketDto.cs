
namespace AdlasHelpDesk.Application.DtoModels
{

    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProblemTypeId { get; set; }
        public Guid? PurchaseSourceId { get; set; }
        public Guid? StoreNameId { get; set; }
        public Guid? LibraryNameId { get; set; }
        public string? OrderNumber { get; set; }
        public string? InvoiceImageUrl { get; set; }
        public string? ProblemDescription { get; set; }
        public string ProblemImageUrl { get; set; } = null!;
        public string? CodesNumber { get; set; }
        public bool? PurchaseDataConfirmed { get; set; }
        public bool AllDataConfirmed { get; set; }
        public string TicketNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public Guid TicketStatusId { get; set; }
    }
}
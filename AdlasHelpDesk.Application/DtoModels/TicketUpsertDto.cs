using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{

    public class TicketUpsertDto
    {
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProblemTypeId { get; set; }
        public Guid? PurchaseSourceId { get; set; }
        public Guid? StoreNameId { get; set; }
        public Guid? LibraryNameId { get; set; }
        public string? OrderNumber { get; set; }
        public string? InvoiceImageUrl { get; set; }
        public string? ProblemDescription { get; set; }
        [Required]
        public string ProblemImageUrl { get; set; } = null!;
        public string? CodesNumber { get; set; }
        public bool? PurchaseDataConfirmed { get; set; }
        public bool AllDataConfirmed { get; set; }
        [Required]
        public string TicketNumber { get; set; } = null!;
        public Guid TicketStatusId { get; set; }
    }
}
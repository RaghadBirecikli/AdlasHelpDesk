using static System.Formats.Asn1.AsnWriter;

namespace AdlasHelpDesk.Domain.Models
{
    public class Ticket : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public Guid ProblemTypeId { get; set; }
        public virtual ProblemType ProblemType { get; set; }

        public Guid? PurchaseSourceId { get; set; }
        public virtual PurchaseSource? PurchaseSource { get; set; }

        public Guid? StoreNameId { get; set; }
        public virtual Store? StoreName { get; set; }

        public Guid? LibraryNameId { get; set; }
        public virtual Library? LibraryName { get; set; }

        public string? OrderNumber { get; set; }
        public string? InvoiceImageUrl { get; set; }      // رابط صورة الفاتورة
        public string? ProblemDescription { get; set; }   // وصف المشكلة
        public string ProblemImageUrl { get; set; }      // صورة للمشكلة
        public string? CodesNumber { get; set; }

        public bool? PurchaseDataConfirmed { get; set; }
        public bool AllDataConfirmed { get; set; }

        public string TicketNumber { get; set; }         // مثل: HD-20250707-001
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid TicketStatusId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
    }
}


namespace AdlasHelpDesk.Domain.Models
{
    public class SendMail : BaseEntity<Guid>
    {
        [MaxLength(200)]
        public string? SenderName { get; set; }
        [MaxLength(50)]
        public string? SenderMail { get; set; }
		public string? Subject { get; set; }
		public string? Message { get; set; }
		public bool? IsSent { get; set; }
		public DateTime? MailDate { get; set; }
    }
}
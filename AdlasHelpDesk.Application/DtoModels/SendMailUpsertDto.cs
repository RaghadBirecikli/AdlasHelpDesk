using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{
    public class SendMailUpsertDto
    {
        public Guid? Id { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string? SenderName { get; set; }
		public string? SenderMail { get; set; }
		public string? Subject { get; set; }
		public string? Message { get; set; }
		public bool? IsSent { get; set; }
		public DateTime? MailDate { get; set; }
	}
}
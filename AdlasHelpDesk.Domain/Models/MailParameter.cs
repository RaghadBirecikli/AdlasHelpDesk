
namespace AdlasHelpDesk.Domain.Models
{
    public class MailParameter : BaseEntity<Guid>
    {
        public string Code { get; set; }
		public string Value { get; set; }
    }
}

namespace AdlasHelpDesk.Application.DtoModels
{

    public class AllowedEmailDomainDto
    {
        public Guid Id { get; set; }
        public string DomainName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
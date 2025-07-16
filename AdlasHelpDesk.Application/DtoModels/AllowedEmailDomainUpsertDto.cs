using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{
    public class AllowedEmailDomainUpsertDto
    {
        public Guid? Id { get; set; }
  
        [Required(ErrorMessage = "Domain name is required.")]
        [MaxLength(100, ErrorMessage = "Domain name must not exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid domain name format. Example: example.com")]
        public string DomainName { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [MaxLength(100)]
        public string? CreatedBy { get; set; }
    }
}
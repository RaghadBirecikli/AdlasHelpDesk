using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{
    public class CompanyUpsertDto
    {
        public Guid? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Logo { get; set; }

        [MaxLength(200)]
        public string? Icon { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        public string? Email { get; set; }
    }
}
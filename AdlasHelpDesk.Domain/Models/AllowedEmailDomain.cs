namespace AdlasHelpDesk.Domain.Models
{
    public class AllowedEmailDomain : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(255)]
        public string DomainName { get; set; } = null!; // مثل: "adlas.com"

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string? CreatedBy { get; set; }
    }
}

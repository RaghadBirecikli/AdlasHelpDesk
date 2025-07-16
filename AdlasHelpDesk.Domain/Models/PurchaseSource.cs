namespace AdlasHelpDesk.Domain.Models
{
    public class PurchaseSource : BaseEntity<Guid>
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}

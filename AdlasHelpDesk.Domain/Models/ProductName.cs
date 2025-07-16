namespace AdlasHelpDesk.Domain.Models
{
    public class ProductName : BaseEntity<Guid>
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;
        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; } = null!;
    }
}

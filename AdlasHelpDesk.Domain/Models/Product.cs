namespace AdlasHelpDesk.Domain.Models
{
    public class Product : BaseEntity<Guid>
    {
        public Guid ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; } = null!;

        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; } = null!;

        public Guid ProductNameId { get; set; }
        public virtual ProductName ProductName { get; set; } = null!;

        public Guid SkillId { get; set; }
        public virtual Skill Skill { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

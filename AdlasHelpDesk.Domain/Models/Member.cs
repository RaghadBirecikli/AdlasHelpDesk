namespace AdlasHelpDesk.Domain.Models
{
    public class Member : BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        [MaxLength(50)]
        public string? JobTitle { get; set; }
       
        [MaxLength(200)]
        public string? FullName { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        public string? Description { get; set; }
		[MaxLength(400)]
		public string? Address { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}

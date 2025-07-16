
namespace AdlasHelpDesk.Domain.Models
{
    public class Company : BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
		public string? Description { get; set; }
        [MaxLength(200)]
        public string? Logo { get; set; }
        [MaxLength(200)]
        public string? Icon { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
   
        [MaxLength(200)]
        public string? Address { get; set; }
       
        [MaxLength(50)]
		public string? Email { get; set; }
     

	}
}
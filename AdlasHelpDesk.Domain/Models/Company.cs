
namespace AdlasHelpDesk.Domain.Models
{
    public class Company : BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
		public string Description { get; set; }
        [MaxLength(200)]
        public string FooterDescription { get; set; }
        [MaxLength(200)]
        public string Logo { get; set; }
        [MaxLength(200)]
        public string Icon { get; set; }
        [MaxLength(20)]
        public string? FirstPhone { get; set; }
        [MaxLength(20)]
        public string? SecondPhone { get; set; }
        [MaxLength(200)]
        public string? FirstAddress { get; set; }
        [MaxLength(200)]
		public string? SecondAddress { get; set; }
        [MaxLength(50)]
		public string? Email { get; set; }
        [MaxLength(200)]
        public string? Instagram { get; set; }
        [MaxLength(200)]
		public string? FaceBook { get; set; }
        [MaxLength(200)]
        public string? Twiter { get; set; }
        [MaxLength(200)]
		public string? YouTube { get; set; }
        [MaxLength(200)]
		public string? Linkedin { get; set; }
        [MaxLength(200)]
		public string? GooglePlus { get; set; }
        [MaxLength(200)]
        public string? AboutUsPageDescription { get; set; }
        [MaxLength(200)]
        public string? SoftwareProductPageDescription { get; set; }
        [MaxLength(200)]
        public string? ContactPageDescription { get; set; }
		public string? AboutUsPageContext { get; set; }

	}
}

namespace AdlasHelpDesk.Application.DtoModels
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        [DisplayName("Nme")]
        public string Name { get; set; }
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [DisplayName("FullName")]
        public string? FullName { get; set; }
        [DisplayName("Email")]
        public string? Email { get; set; }
        public string? JobTitle { get; set; }
        public string? JobTitleEn { get; set; }
        public string? JobTitleTr { get; set; }
        public string? JobTitleDe { get; set; }
        public string? JobTitleTranslated { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DisplayName("IsActive")]
        public bool IsActive { get; set; }
        [DisplayName("IsAdmin")]
        public bool IsAdmin { get; set; }
        public string IsActiveStatus => IsActive ? "Aktif" : "Pasif";
        public string IsAdminStatus => IsAdmin ? "Admin" : "Personel";
        public string? Image { get; set; }
		public string? Phone { get; set; }
		public string? Description { get; set; }
		public bool IsShownOnWebsite { get; set; }
		public int? Sort { get; set; }
		public string? Instagram { get; set; }
		public string? FaceBook { get; set; }
		public string? Twiter { get; set; }
		public string? Linkedin { get; set; }
		public string? Address { get; set; }
	}
}

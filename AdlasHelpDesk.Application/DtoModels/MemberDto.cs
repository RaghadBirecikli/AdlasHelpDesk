
namespace AdlasHelpDesk.Application.DtoModels
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } // مجمّع من الاسم واللقب
        public string? Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string? JobTitle { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; } // اسم الشركة (اختياري للعرض)
    }

}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Application.DtoModels
{
    public class MemberDetailsUpsertDto
    {
        public Guid? Id { get; set; }
        [StringLength(200)]
        public string? FullName { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numaranızı Kontrol Ediniz.11 Karakter Olmalı.")]
        [Display(Name = "T.C. Kimlik No")]
        public string? TckNo { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Gender { get; set; }
        public DateTime? JobStartDate { get; set; }
        public DateTime? JobEndDate { get; set; }
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefon numarası ugun değil")]
        public string? MobilePhone { get; set; }
        [StringLength(20)]
        public string? HomePhone { get; set; }
        [DisplayName("Departman")]
        public int? DepartmentId { get; set; }
        [DisplayName("Unvan")]
        public int? JobTitleId { get; set; }
       
        [StringLength(200)]
        public string? RelativeMemberFullName { get; set; }
        [StringLength(20)]
        public string? RelativeMemberPhone { get; set; }
        [StringLength(50)]
        public string? RelativeMemberDegree { get; set; }
        public int? MemberRoleId { get; set; }
        public SelectList? Departments { get; set; }
        public SelectList? JobTitles { get; set; }
        public SelectList? MemberRoles { get; set; }
    }
}

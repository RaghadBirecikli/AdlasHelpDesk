using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdlasHelpDesk.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Application.DtoModels
{
    public class MemberUpsertDto
    {
        public Guid? Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required(ErrorMessage = "Ad Alanı Boş Geçilemez")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required(ErrorMessage = "Soyad Alanı Boş Geçilemez")]
        public string Surname { get; set; }
        [StringLength(200, MinimumLength = 2)]
        public string? FullName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required(ErrorMessage = "Kullanıcı Adı Alanı Boş Geçilemez")]
        public string UserName { get; set; }
        [StringLength(200)]
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }
        public string? JobTitle { get; set; } // default
        public string? JobTitleEn { get; set; } // English
        public string? JobTitleTr { get; set; } // Turkish
        public string? JobTitleDe { get; set; } // Dutch
        [StringLength(300)]
        public string? Image { get; set; }

        [DataType(DataType.Password)]
        [Compare("RValidatePassword", ErrorMessage = "Şifreler Aynı Değildir")]
        public string? ValidatePassword { get; set; }
        [DataType(DataType.Password)]
        public string? RValidatePassword { get; set; }
        public bool isUpsertPassword { get; set; }

        public IFormFile? ImageFile { get; set; }
		[MaxLength(20)]
		public string? Phone { get; set; }
		public string? Description { get; set; }
		public bool IsShownOnWebsite { get; set; }
		public int? Sort { get; set; }
		[MaxLength(200)]
		public string? Instagram { get; set; }
		[MaxLength(200)]
		public string? FaceBook { get; set; }
		[MaxLength(200)]
		public string? Twiter { get; set; }
		[MaxLength(200)]
		public string? Linkedin { get; set; }

		[MaxLength(400)]
		public string? Address { get; set; }
	}
}

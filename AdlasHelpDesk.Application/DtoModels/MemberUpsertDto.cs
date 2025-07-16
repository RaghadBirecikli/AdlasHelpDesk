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
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; } // يفضل أن يُرسل مشفرًا أو يتم تشفيره قبل التخزين

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;

        [MaxLength(50)]
        public string? JobTitle { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public string? Description { get; set; }

        [MaxLength(400)]
        public string? Address { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
    }
}

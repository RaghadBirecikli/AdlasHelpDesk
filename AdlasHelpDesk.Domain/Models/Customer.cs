using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Domain.Models
{
    public class Customer : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string VerificationCode { get; set; }
        public string UniversityNumber { get; set; }

        public Guid UniversityId { get; set; }
        public virtual University University { get; set; }

    }
}

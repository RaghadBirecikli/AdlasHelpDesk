using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{
 
        public class CustomerUpsertDto
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string UniversityNumber { get; set; }
            public Guid UniversityId { get; set; }

        
            public string VerificationCode { get; set; }
            public bool IsEmailVerified { get; set; }
        }
    }
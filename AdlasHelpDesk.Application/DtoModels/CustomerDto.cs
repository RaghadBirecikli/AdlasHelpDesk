
namespace AdlasHelpDesk.Application.DtoModels
{

    public class CustomerDto
    {
        public Guid Id { get; set; } // ?? BaseEntity
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string UniversityNumber { get; set; }

        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}
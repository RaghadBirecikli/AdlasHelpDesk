using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{

    public class SkillUpsertDto
    {
        public Guid? Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public Guid PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
using Microsoft.AspNetCore.Http;

namespace AdlasHelpDesk.Application.DtoModels
{

    public class ProductUpsertDto
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid ProductTypeId { get; set; }

        [Required]
        public Guid PublisherId { get; set; }

        [Required]
        public Guid ProductNameId { get; set; }

        [Required]
        public Guid SkillId { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
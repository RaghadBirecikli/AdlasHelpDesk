
namespace AdlasHelpDesk.Application.DtoModels
{

    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid ProductTypeId { get; set; }
        public string? ProductTypeName { get; set; }
        public Guid PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public Guid ProductNameId { get; set; }
        public string? ProductNameName { get; set; }
        public Guid SkillId { get; set; }
        public string? SkillName { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
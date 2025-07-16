
namespace AdlasHelpDesk.Application.DtoModels
{

    public class SkillDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? PublisherName { get; set; }
        public Guid PublisherId { get; set; }
    }
}

namespace AdlasHelpDesk.Application.DtoModels
{

    public class TicketStatusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
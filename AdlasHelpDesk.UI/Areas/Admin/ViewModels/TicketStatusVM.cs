using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class TicketStatusVM
    {
        public List<TicketStatusDto> Entities { get; set; }
    }
}

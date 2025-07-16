using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class PublisherVM
    {
        public List<PublisherDto> Entities { get; set; }
    }
}

using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class LibraryVM
    {
        public List<LibraryDto> Entities { get; set; }
    }
}

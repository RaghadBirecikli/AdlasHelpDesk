using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class ProductNameVM
    {
        public List<ProductNameDto> Entities { get; set; }
    }
}

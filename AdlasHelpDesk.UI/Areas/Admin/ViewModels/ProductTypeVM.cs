using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class ProductTypeVM
    {
        public List<ProductTypeDto> Entities { get; set; }
    }
}

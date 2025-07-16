using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class ProductNameUpsertVM
    {
        public ProductNameUpsertDto ProductNameUpsertDto { get; set; }
        public SelectList Publishers { get; set; } 
    }
}

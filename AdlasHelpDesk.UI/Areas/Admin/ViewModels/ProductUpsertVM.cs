using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class ProductUpsertVM
    {
        public ProductUpsertDto ProductUpsertDto { get; set; }
        public SelectList Publishers { get; set; } 
        public SelectList ProductTypes { get; set; } 
        public SelectList ProductNames { get; set; } 
        public SelectList Skills { get; set; } 
    }
}

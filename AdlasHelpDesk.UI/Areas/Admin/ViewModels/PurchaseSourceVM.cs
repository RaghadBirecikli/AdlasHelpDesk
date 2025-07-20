using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class PurchaseSourceVM
    {
        public List<PurchaseSourceDto> Entities { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public interface IPagingVM
    {
        public PagingParameters PagingParameters { get; }
        public List<SelectListItem> OrderByItems { get; set; }
    }
}

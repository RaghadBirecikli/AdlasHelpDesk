using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class MemberPageVM : IPagingVM
    {
        public PagingParameters PagingParameters { get; set; }
        public List<MemberDto> Entities { get; set; }
        public List<SelectListItem> OrderByItems { get; set; } = new List<SelectListItem>();
        public string PublicFilter { get; set; }
        public bool? isActive { get; set; }
    }
}

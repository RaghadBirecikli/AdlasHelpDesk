using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class UniversityVM 
    {
        public List<UniversityDto> Entities { get; set; }
        public string PublicFilter { get; set; }
        public bool? isActive { get; set; }
    }
}

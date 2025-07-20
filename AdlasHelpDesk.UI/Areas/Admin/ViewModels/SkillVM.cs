using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class SkillVM
    {
        public List<SkillDto> Entities { get; set; }
    }
}

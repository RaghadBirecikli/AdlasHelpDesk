using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class SkillUpsertVM
    {
        public SkillUpsertDto SkillUpsertDto { get; set; }
        public SelectList Publishers { get; set; } 
    }
}

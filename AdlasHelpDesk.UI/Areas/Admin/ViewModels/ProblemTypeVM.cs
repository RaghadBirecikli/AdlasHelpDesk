using AdlasHelpDesk.Application.DtoModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdlasHelpDesk.UI.Areas.Admin.ViewModels
{
    public class ProblemTypeVM
    {
        public List<ProblemTypeDto> Entities { get; set; }
    }
}

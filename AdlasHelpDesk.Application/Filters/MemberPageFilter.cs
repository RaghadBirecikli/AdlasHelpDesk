using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlasHelpDesk.Application.Filters
{
    public class MemberPageFilter
    {
        public MemberPageFilter()
        {
            PagingResult = new PagingResult<MemberDto>();
        }
        public PagingResult<MemberDto> PagingResult { get; set; }
        public string PublicFilter { get; set; }
        public bool? isActive { get; set; }

    }
}

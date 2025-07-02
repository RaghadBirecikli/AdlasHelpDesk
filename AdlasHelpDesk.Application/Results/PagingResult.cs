using System.Collections.Generic;

namespace AdlasHelpDesk.Application.Results
{
    public class PagingResult<T>
    {
        public PagingParameters? PagingParameters { get; set; }
        public List<T> Entities { get; set; }
        public Meta? Meta { get; set; }
    }
}

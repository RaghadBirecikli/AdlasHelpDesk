using System;

namespace AdlasHelpDesk.Application.Results
{
    public class PagingParameters
    {
        public PagingParameters() { }
        public PagingParameters(string orderBy)
        {
            Page = 1;
            PageSize = 100;
            IsDesc = true;
            OrderBy = orderBy;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = string.Empty;
        public bool IsDesc { get; set; }

        public Nullable<int> TotalCount { get; set; }
        public Nullable<int> CurrentPageSize { get; set; }
        public Nullable<int> TotalPages { get; set; }
    }
}

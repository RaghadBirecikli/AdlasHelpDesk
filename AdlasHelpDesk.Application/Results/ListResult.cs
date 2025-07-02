using System.Collections.Generic;

namespace AdlasHelpDesk.Application.Results
{
    public class ListResult<TEntity>
    {
        public Meta Meta { get; set; }
        public List<TEntity>? Entities { get; set; }

        public ListResult(Meta meta, List<TEntity> entities) : this(meta)
        {
            Entities = entities;
        }
        public ListResult(Meta meta)
        {
            Meta = meta;
        }
    }
}

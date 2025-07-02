namespace AdlasHelpDesk.Application.Results
{
    public class ObjectResult<TEntity>
    {
        public Meta Meta { get; set; }
        public TEntity? Entity { get; set; }

        public ObjectResult(Meta meta, TEntity entity) : this(meta)
        {
            Entity = entity;
        }

        public ObjectResult(Meta meta)
        {
            Meta = meta;
        }

    }
}

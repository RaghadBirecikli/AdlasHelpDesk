namespace AdlasHelpDesk.Application.Results
{
    public class Result
    {
        public Meta? Meta { get; set; }
        public Result(Meta meta)
        {
            Meta = meta;
        }
    }
}

namespace AdlasHelpDesk.Application.Interfaces.Base
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}

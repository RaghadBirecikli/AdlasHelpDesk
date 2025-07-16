
namespace AdlasHelpDesk.Application.Services
{
    public interface ILibraryService
    {
        Task<ObjectResult<LibraryUpsertDto>> Get(Guid id);
        Task<ListResult<LibraryDto>> GetAll();
        Task<ListResult<LibraryDto>> GetList();
        Task<ObjectResult<LibraryUpsertDto>> Add(LibraryUpsertDto model);
        Task<ObjectResult<LibraryUpsertDto>> Update(LibraryUpsertDto model);
        Task<Result> Delete(Guid id);

    }
}

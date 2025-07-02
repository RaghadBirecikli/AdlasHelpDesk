using AdlasHelpDesk.Application.Enums;
using AdlasHelpDesk.Domain.Common;
using System.Linq.Expressions;

namespace AdlasHelpDesk.Application.Interfaces.Base
{
	public interface IBaseRepository<T> where T : IBaseEntity
	{
		Task<T> GetByIdAsync(Guid id);
		Task<T> GetByIdAsync(int id);
		Task<T> GetByIdAsync(byte id);
		Task<T> GetAsync(Expression<Func<T, bool>> filter);
		Task<T> AddAsync(T entity);
		Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
		IEnumerable<T> UpdateRange(IEnumerable<T> entities);
		Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
		Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
		void Delete(T entity);
		//void DeleteWithTranslations(T entity);
		void DeleteList(IEnumerable<T> entities);
		void DeleteList(Expression<Func<T, bool>> filter);
		Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter, Expression<Func<T, object>> orderBy, bool isDesc);
		Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);
		T Update(T entity);
		Task<PagingResult<T>> GetPage(PagingResult<T> pagingModel, Expression<Func<T, bool>> filter = null);
		//Task<IEnumerable<Translation>> AddTranlations(T model, object ent, List<string> attributes);
		//Task<IEnumerable<Translation>> UpdateTranslations(T model, object ent, List<string> attributes);
		//void GetWithTranslations(T entity, object model, List<string> attributes);
		//void GetWithTranslations(string TableName, Guid entityId, object model, List<string> attributes);

	}
}

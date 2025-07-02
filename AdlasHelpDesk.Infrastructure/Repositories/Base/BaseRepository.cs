using Microsoft.EntityFrameworkCore.Query;
using AdlasHelpDesk.Domain.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Castle.Core.Resource;
using AdlasHelpDesk.Application.Enums;
using AdlasHelpDesk.Application.IRepositories;
using System;
using AdlasHelpDesk.Application.Interfaces;
using System.Globalization;
using AdlasHelpDesk.Domain.Models;

namespace AdlasHelpDesk.Infrastructure.Repositories.Base
{
	public abstract class BaseRepository<T> : IBaseRepository<T>
			  where T : class, IBaseEntity, new()
	{
		private readonly ApplicationDbContext _context;

		public BaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<T> GetByIdAsync(Guid id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<T> GetByIdAsync(byte id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
		{
			return await _context.Set<T>().SingleOrDefaultAsync(filter);
		}
		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return entity;
		}

		public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
		{
			await _context.Set<T>().AddRangeAsync(entities);
			return entities;
		}
		public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
		{
			_context.Set<T>().UpdateRange(entities);
			return entities;
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null)
		{
			return await _context.Set<T>().AnyAsync(filter);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
		{
			return await _context.Set<T>().CountAsync(filter);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}
		public void DeleteList(IEnumerable<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
		}
		public void DeleteList(Expression<Func<T, bool>> filter)
		{
			_context.Set<T>().RemoveRange(_context.Set<T>().Where(filter));
		}
		public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter, Expression<Func<T, object>> orderBy, bool isDesc)
		{
			var list = filter == null
						 ? _context.Set<T>()
						 : _context.Set<T>().Where(filter);
			list = isDesc ? list.OrderByDescending(orderBy) : list = list.OrderBy(orderBy);

			return await list.ToListAsync();
		}
		public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter = null)
		{

			var list = filter == null
						 ? _context.Set<T>()
						 : _context.Set<T>().Where(filter);

			return await list.ToListAsync();
		}

		public T Update(T entity)
		{
			try
			{
				_context.Set<T>().Update(entity);
				return entity;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public async Task<PagingResult<T>> GetPage(PagingResult<T> pagingModel, Expression<Func<T, bool>>? filter = null)
		{

			IQueryable<T> list = filter == null
				? _context.Set<T>()
				: _context.Set<T>().Where(filter);

			if (pagingModel.PagingParameters.OrderBy != null)
				list = pagingModel.PagingParameters.IsDesc
				? list.OrderByDescending(ToLambda<T>(pagingModel.PagingParameters.OrderBy))
				: list.OrderBy(ToLambda<T>(pagingModel.PagingParameters.OrderBy));


			pagingModel.PagingParameters.TotalCount = list.Count();
			pagingModel.PagingParameters.TotalPages = pagingModel.PagingParameters.PageSize != 0 ? ((int)Math.Ceiling(pagingModel.PagingParameters.TotalCount.Value / (double)pagingModel.PagingParameters.PageSize)) : 1;
			//pagingModel.PagingParameters.TotalPages = pagingModel.PagingParameters.TotalPages == 0 ? 1 : pagingModel.PagingParameters.TotalPages;
			pagingModel.Entities = pagingModel.PagingParameters.PageSize != 0 ? (await list.Skip((pagingModel.PagingParameters.Page - 1) * pagingModel.PagingParameters.PageSize).Take(pagingModel.PagingParameters.PageSize).ToListAsync()) : new List<T>();
			pagingModel.PagingParameters.CurrentPageSize = pagingModel.Entities.Count();
			pagingModel.Meta = Meta.Success();

			return pagingModel;
		}
		private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
		{
			var parameter = Expression.Parameter(typeof(T));
			var property = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);

			var propAsObject = Expression.Convert(property, typeof(object));

			return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
		}
		//public async Task<IEnumerable<Translation>> AddTranlations(T ent, object model, List<string> attributes)
		//{
		//	Type tableClass = typeof(T);
		//	string TableName = tableClass.Name;
		//	List<Translation> translations = new List<Translation>();
		//	foreach (string attribute in attributes)
		//	{
		//		foreach (LanguagesEnum language in Enum.GetValues(typeof(LanguagesEnum)))
		//		{
		//			string propertyName = attribute + language.ToString();
		//			string propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model) as string;
		//			PropertyInfo idProperty = tableClass.GetProperty("Id");
		//			if (idProperty != null)
		//			{
		//				object idValue = idProperty.GetValue(ent);
		//				if (idValue is Guid entityId)
		//					if (!string.IsNullOrEmpty(propertyValue))
		//						translations.Add(new Translation(language.ToString(), TableName, entityId, attribute, propertyValue));
		//			}
		//		}
		//	}
		//	await _context.Translation.AddRangeAsync(translations);
		//	return translations;
		//}
		//public async Task<IEnumerable<Translation>> UpdateTranslations(T ent, object model, List<string> attributes)
		//{
		//	Type tableClass = typeof(T);
		//	string TableName = tableClass.Name;
		//	List<Translation> translations = new List<Translation>();

		//	PropertyInfo idProperty = tableClass.GetProperty("Id");
		//	if (idProperty != null)
		//	{
		//		object idValue = idProperty.GetValue(ent);
		//		if (idValue is Guid entityId)
		//		{
		//			IQueryable<Translation> list = _context.Translation.Where(t => t.RecID == entityId && t.TableName == TableName);
		//			foreach (string attribute in attributes)
		//			{
		//				foreach (LanguagesEnum language in Enum.GetValues(typeof(LanguagesEnum)))
		//				{
		//					string propertyName = attribute + language.ToString();
		//					string propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model) as string;
		//					Translation existingTranslation = list.FirstOrDefault(t => t.AttributeName == attribute && t.LanguageCode == language.ToString());

		//					if (existingTranslation != null)
		//						existingTranslation.Value = propertyValue;
		//					else if (!string.IsNullOrEmpty(propertyValue))
		//						translations.Add(new Translation(language.ToString(), TableName, entityId, attribute, propertyValue));
		//				}
		//			}
		//		}
		//		if (translations.Any())
		//			await _context.Translation.AddRangeAsync(translations);
		//	}
		//	return translations;
		//}
		//public void GetWithTranslations(T entity, object model, List<string> attributes)
		//{
		//	Type tableClass = typeof(T);
		//	string TableName = tableClass.Name;
		//	PropertyInfo idProperty = tableClass.GetProperty("Id");
		//	string capitalized = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Thread.CurrentThread.CurrentCulture.Name);
		//	if (idProperty != null)
		//	{
		//		object idValue = idProperty.GetValue(entity);
		//		if (idValue is Guid entityId)
		//		{
		//			IQueryable<Translation> translations = _context.Translation.Where(x => x.RecID == entityId && x.TableName == TableName);
		//			foreach (string attribute in attributes)
		//			{
		//				string translationKey = $"{attribute}{capitalized}";
		//				PropertyInfo? translatedProp = model.GetType().GetProperty(attribute + "Translated");

		//				foreach (LanguagesEnum language in Enum.GetValues(typeof(LanguagesEnum)))
		//				{
		//					string propertyName = attribute + language.ToString();
		//					Translation translation = translations.FirstOrDefault(t =>
		//						t.AttributeName == attribute && t.LanguageCode == language.ToString());

		//					if (translation != null)
		//					{
		//						model.GetType().GetProperty(propertyName)?.SetValue(model, translation.Value);
		//						if (capitalized == language.ToString())
		//							translatedProp?.SetValue(model, translation.Value);
		//					}
		//				}
		//				if (string.IsNullOrEmpty((string?)translatedProp?.GetValue(model)))
		//					translatedProp?.SetValue(model, model.GetType().GetProperty(attribute));
		//			}
		//		}
		//	}
		//}
		//public void GetWithTranslations(string TableName, Guid entityId, object model, List<string> attributes)
		//{
		//	string capitalized = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Thread.CurrentThread.CurrentCulture.Name);
		//	IQueryable<Translation> translations = _context.Translation.Where(x => x.RecID == entityId && x.TableName == TableName);
		//	foreach (string attribute in attributes)
		//	{
		//		string translationKey = $"{attribute}{capitalized}";
		//		PropertyInfo? translatedProp = model.GetType().GetProperty(attribute + "Translated");

		//		foreach (LanguagesEnum language in Enum.GetValues(typeof(LanguagesEnum)))
		//		{
		//			string propertyName = attribute + language.ToString();
		//			Translation translation = translations.FirstOrDefault(t =>
		//				t.AttributeName == attribute && t.LanguageCode == language.ToString());

		//			if (translation != null)
		//			{
		//				model.GetType().GetProperty(propertyName)?.SetValue(model, translation.Value);
		//				if (capitalized == language.ToString())
		//					translatedProp?.SetValue(model, translation.Value);
		//			}
		//		}
		//		if (string.IsNullOrEmpty((string?)translatedProp?.GetValue(model)))
		//			translatedProp?.SetValue(model, model.GetType().GetProperty(attribute)?.GetValue(model));
		//	}
		//}


		//public void DeleteWithTranslations(T entity)
		//{
		//	Type tableClass = typeof(T);
		//	string TableName = tableClass.Name;
		//	PropertyInfo idProperty = tableClass.GetProperty("Id");

		//	if (idProperty != null)
		//	{
		//		object idValue = idProperty.GetValue(entity);
		//		if (idValue is Guid entityId)
		//		{
		//			IQueryable<Translation> translations = _context.Translation.Where(x => x.RecID == entityId && x.TableName == TableName);
		//			if (translations.Any())
		//				_context.Translation.RemoveRange(translations);

		//			_context.Set<T>().Remove(entity);
		//		}
		//	}
		//}
	}
}

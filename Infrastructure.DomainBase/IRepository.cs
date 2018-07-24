using System.Collections.Generic;

namespace Infrastructure.DomainBase
{
	/// <summary>
	///     Репозиторий
	/// </summary>
	/// <typeparam name="T">Тип сущности</typeparam>
	/// <typeparam name="TWorkItemStrategy"></typeparam>
	public interface IRepository<T, in TWorkItemStrategy>
		where T : IEntity
		where TWorkItemStrategy : WorkItemStrategy
	{
		/// <summary>
		///     Получить экземпляр сущности по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <param name="workItemStrategy"></param>
		/// <returns>Восстановленный экземпляр сущности</returns>
		T Get(int id, TWorkItemStrategy workItemStrategy = null);

		/// <summary>
		///     Получить все экземпляры сущности
		/// </summary>
		/// <returns>Список восстановленных экземпляров сущности</returns>
		IReadOnlyCollection<T> GetAll(TWorkItemStrategy workItemStrategy = null);

		/// <summary>
		///     Сохранить или заменить экземпляр сущности в репозитории
		/// </summary>
		/// <param name="entity">Экземпляр сущности</param>
		/// <param name="workItemStrategy"></param>
		/// <returns>Флаг успешности выполнения операции</returns>
		bool Save(T entity, TWorkItemStrategy workItemStrategy = null);

		/// <summary>
		///     Удалить экземпляр сущности из репозитория
		/// </summary>
		/// <param name="entity">Экземпляр сущности</param>
		/// <returns>Флаг успешности выполнения операции</returns>
		bool Remove(T entity);

		/// <summary>
		///     Получить несколько экземпляров сущностей
		/// </summary>
		/// <param name="ids">Список идентификаторов</param>
		/// <param name="workItemStrategy"></param>
		/// <returns>Список сущностей</returns>
		IReadOnlyCollection<T> GetMany(IEnumerable<int> ids, TWorkItemStrategy workItemStrategy = null);
	}
}
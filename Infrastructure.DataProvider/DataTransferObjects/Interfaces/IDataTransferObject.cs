using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider
{
	/// <summary>
	///     Интерфейс DTO для сущности
	/// </summary>
	/// <typeparam name="TEntity">Тип сущности</typeparam>
	/// <typeparam name="TWorkItemStrategy"></typeparam>
	public interface IDataTransferObject<TEntity, in TWorkItemStrategy>
		where TEntity : IEntity
		where TWorkItemStrategy : WorkItemStrategy
	{
		int Id { get; set; }

		bool Deleted { get; set; }

		TEntity Reconstitute(TWorkItemStrategy workItemStrategy);

		void Update(TEntity entity, TWorkItemStrategy workItemStrategy);
	}
}
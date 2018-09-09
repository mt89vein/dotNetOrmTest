using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain.Services
{
    /// <summary>
    /// Базовый интерфейс сервисов.
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IBaseService<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Вернет запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="workItemStrategy">Стратегия</param>
        /// <returns></returns>
        TEntity Get(int id, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        /// <summary>
        /// Получить сущности по идентификаторам
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <param name="workItemStrategy">Стратегия</param>
        /// <returns></returns>
        IReadOnlyCollection<TEntity> Get(IEnumerable<int> ids, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        /// <summary>
        /// Получить все
        /// </summary>
        /// <param name="workItemStrategy">Стратегия</param>
        /// <returns></returns>
        IReadOnlyCollection<TEntity> Get(IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <param name="workItemStrategy">Стратегия</param>
        void Save(TEntity entity, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        /// <summary>
        /// Удаление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Remove(int id);

        /// <summary>
        /// Удаление записи.
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Remove(TEntity entity);
    }
}
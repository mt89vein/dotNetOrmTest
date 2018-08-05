using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain.Services
{
    /// <summary>
    /// Базовый интерфейс сервисов.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TWorkItemStrategy"></typeparam>
    public interface IBaseService<TEntity, in TWorkItemStrategy>
        where TEntity : Entity
        where TWorkItemStrategy : WorkItemStrategy
    {
        /// <summary>
        /// Вернет запись по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        TEntity Get(int id, TWorkItemStrategy workItemStrategy = null);

        IReadOnlyCollection<TEntity> Get(IEnumerable<int> ids, TWorkItemStrategy workItemStrategy = null);

        /// <summary>
        /// Получить все
        /// </summary>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        IReadOnlyCollection<TEntity> Get(TWorkItemStrategy workItemStrategy = null);

        /// <summary>
        /// Добавление записи.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Insert(TEntity entity);

        /// <summary>
        /// Множественное добавление записей.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Обновление записи.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Обновление записей.
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удаление по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Удаление записи.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Удаление записей.
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удаление записей по идентификаторам
        /// </summary>
        /// <param name="ids">Идентификатороы</param>
        void Delete(int[] ids);
    }
}
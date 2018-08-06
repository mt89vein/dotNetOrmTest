using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain.Services
{
    /// <summary>
    /// Базовый интерфейс сервисов.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TWorkItemStrategy"></typeparam>
    public interface IBaseService<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Вернет запись по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        TEntity Get(int id, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        IReadOnlyCollection<TEntity> Get(IEnumerable<int> ids, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        /// <summary>
        /// Получить все
        /// </summary>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        IReadOnlyCollection<TEntity> Get(IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

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
        /// <param name="workItemStrategy"></param>
        void Update(TEntity entity, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

        /// <summary>
        /// Обновление записей.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="workItemStrategy"></param>
        void Update(IEnumerable<TEntity> entities, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy));

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
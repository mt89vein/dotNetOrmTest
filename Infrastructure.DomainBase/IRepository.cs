using System.Collections.Generic;

namespace Infrastructure.DomainBase
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Получить экземпляр сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Восстановленный экземпляр сущности</returns>
        T Get(int id);

        /// <summary>
        /// Получить все экземпляры сущности
        /// </summary>
        /// <returns>Список восстановленных экземпляров сущности</returns>
        IReadOnlyCollection<T> GetAll();

        /// <summary>
        /// Сохранить или заменить экземпляр сущности в репозитории
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        /// <returns>Флаг успешности выполнения операции</returns>
        bool Save(T entity);

        /// <summary>
        /// Удалить экземпляр сущности из репозитория
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        /// <returns>Флаг успешности выполнения операции</returns>
        bool Remove(T entity);

        /// <summary>
        /// Получить несколько экземпляров сущностей
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <returns>Список сущностей</returns>
        IReadOnlyCollection<T> GetMany(IEnumerable<int> ids);
    }
}
using System.Collections.Generic;
using Infrastructure.DataProvider;
using Infrastructure.DomainBase;

namespace Domain.Services
{
    /// <summary>
    /// Сервис для кэширования
    /// </summary>
    /// <typeparam name="TDto">Объект кэширования</typeparam>
    /// <typeparam name="TDomainEntity">Доменный объект</typeparam>
    public interface ICacheService<TDto, TDomainEntity>
        where TDto : IDataTransferObject<TDomainEntity>
        where TDomainEntity : IEntity
    {
        /// <summary>
        /// Проверить, хранится ли сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        bool HasKey(int id);

        /// <summary>
        /// Проверить, хранится ли в кэше сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>True, если хранится</returns>
        bool HasKey(TDto entity);

        /// <summary>
        /// Получить из кэша по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        TDto Get(int id);

        /// <summary>
        /// Получить из кэша несколько экземпляров сущности по списку идентификаторов
        /// </summary>
        /// <param name="ids">Список идентификаторв</param>
        /// <returns>Список сущностей</returns>
        IReadOnlyCollection<TDto> Get(int[] ids);

        /// <summary>
        /// Положить в кэш сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Set(TDto entity);

        /// <summary>
        /// Положить в кэш сущности
        /// </summary>
        /// <param name="entities">Сущности</param>
        void Set(IEnumerable<TDto> entities);

        /// <summary>
        /// Удалить из кэша по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Удалить из кэша по идентификатору
        /// </summary>
        /// <param name="ids">Идентификаторы</param>
        void Delete(int[] ids);

        /// <summary>
        /// Удалить из кэша сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Delete(TDto entity);

        /// <summary>
        /// Удалить из кэша сущности
        /// </summary>
        /// <param name="entities">Сущности</param>
        void Delete(IEnumerable<TDto> entities);
    }
}
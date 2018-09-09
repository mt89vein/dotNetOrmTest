using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure.DataProvider;

namespace Infrastructure.DomainBase
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="TDomainEntity">Тип сущности</typeparam>
    /// <typeparam name="TSpecification"></typeparam>
    /// <typeparam name="TDto">DTO</typeparam>
    public interface IRepository<TDomainEntity, TDto, in TSpecification>
        where TDto : class, IDataTransferObject<TDomainEntity>
        where TSpecification : ISpecification<TDto>
    {
        /// <summary>
        /// Получить экземпляр сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="specification"></param>
        /// <returns>Восстановленный экземпляр сущности</returns>
        TDto Get(int id, TSpecification specification = default(TSpecification));

        /// <summary>
        /// Получить несколько экземпляров сущностей
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <param name="specification"></param>
        /// <returns>Список сущностей</returns>
        IReadOnlyCollection<TDto> Get(IEnumerable<int> ids, TSpecification specification = default(TSpecification));

        /// <summary>
        /// Получить все экземпляры сущности
        /// </summary>
        /// <returns>Список экземпляров сущности</returns>
        IReadOnlyCollection<TDto> Get(TSpecification specification = default(TSpecification));

        /// <summary>
        /// Получить сущности по предикату и спецификации
        /// </summary>
        /// <param name="readOnly">Получить сущности только для чтения</param>
        /// <param name="specification">Спецификация</param>
        /// <returns></returns>
        IEnumerable<TDto> GetBySpecification(bool readOnly = true,
            TSpecification specification = default(TSpecification));

        /// <summary>
        /// Удалить сущность по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Remove(int id);

        /// <summary>
        /// Удалить экземпляр сущности из репозитория
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        /// <returns>Флаг успешности выполнения операции</returns>
        void Remove(TDto entity);

        /// <summary>
        /// Удалить несколько сущностей по идентификатору
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        void Remove(int[] ids);

        /// <summary>
        /// Сохранить сущность по стратегии
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateItemStrategy"></param>
        void Save(TDto entity, IWorkItemStrategy updateItemStrategy = null);
    }
}
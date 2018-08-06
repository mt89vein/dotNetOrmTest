using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="TDto">Тип сущности</typeparam>
    /// <typeparam name="TSpecification"></typeparam>
    /// <typeparam name="TDomainEntity"></typeparam>
    public abstract class
        Repository<TDomainEntity, TDto, TSpecification> : IRepository<TDomainEntity, TDto, TSpecification>
        where TDto : class, IDataTransferObject<TDomainEntity>
        where TSpecification : ISpecification<TDto>
    {
        public abstract IQueryable<TDto> Table(bool readOnly = false);

        /// <summary>
        /// Получить экземпляр сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="specification"></param>
        /// <returns>Восстановленный экземпляр сущности</returns>
        public abstract TDto Get(int id, TSpecification specification = default(TSpecification));

        /// <summary>
        /// Получить несколько экземпляров сущностей
        /// </summary>
        /// <param name="ids">Список идентификаторов</param>
        /// <param name="specification"></param>
        /// <returns>Список сущностей</returns>
        public abstract IReadOnlyCollection<TDto> Get(IEnumerable<int> ids,
            TSpecification specification = default(TSpecification));

        /// <summary>
        /// Получить все экземпляры сущности
        /// </summary>
        /// <returns>Список экземпляров сущности</returns>
        public abstract IReadOnlyCollection<TDto> GetAll(TSpecification specification = default(TSpecification));

        /// <summary>
        /// Получить сущности по спецификации
        /// </summary>
        /// <param name="readOnly">Только для чтения</param>
        /// <param name="specification">Спецификация</param>
        /// <returns></returns>
        public abstract IEnumerable<TDto> GetBySpecification(bool readOnly = true,
            TSpecification specification = default(TSpecification));

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Add(TDto entity);

        /// <summary>
        /// Удалить по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract void Remove(int id);

        /// <summary>
        /// Удалить экземпляр сущности из репозитория
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        /// <returns>Флаг успешности выполнения операции</returns>
        public abstract void Remove(TDto entity);

        /// <summary>
        /// Удалить по идентификаторам
        /// </summary>
        /// <param name="ids"></param>
        public abstract void Remove(int[] ids);

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateWorkItemStrategy"></param>
        public abstract void Update(TDto entity, IWorkItemStrategy updateWorkItemStrategy = null);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public abstract class
        LinqRepository<TDomainEntity, TDto, TSpecification> : Repository<TDomainEntity, TDto, TSpecification>
        where TDomainEntity : Entity
        where TDto : class, IDataTransferObject<TDomainEntity>, new()
        where TSpecification : ISpecification<TDto>
    {
        protected readonly ApplicationContext Context;
        private DbSet<TDto> _entities;

        protected LinqRepository(ApplicationContext context)
        {
            Context = context;
        }

        protected virtual IQueryable<TDto> QueryAll => DbSetTable;

        protected DbSet<TDto> DbSetTable => _entities ?? (_entities = Context.Set<TDto>());

        /// <summary>
        /// Таблица с данными
        /// </summary>
        public override IQueryable<TDto> Table(bool readOnly = false)
        {
            return readOnly
                ? DbSetTable.AsNoTracking()
                : DbSetTable;
        }

        public override void Add(TDto entity)
        {
            if (entity != null)
            {
                DbSetTable.Add(entity);
            }
        }

        public override void Remove(int id)
        {
            var dto = new TDto {Id = id};

            DbSetTable.Attach(dto);
            DbSetTable.Remove(dto);
        }

        public override void Remove(int[] ids)
        {
            ids.ToList().ForEach(Remove);
        }

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public override void Remove(TDto entity)
        {
            if (entity != null)
            {
                Remove(entity.Id);
            }
        }

        public override IEnumerable<TDto> GetBySpecification(bool readOnly = true,
            TSpecification specification = default(TSpecification))
        {
            if (specification == null)
            {
                BaseQuery(readOnly: readOnly);
            }
            return BaseQuery(specification, readOnly);
        }

        protected IQueryable<TDto> BaseQuery(ISpecification<TDto> specification = null, bool readOnly = false)
        {
            if (specification == null)
            {
                return Table(readOnly);
            }

            var query = specification.SatisfyingEntitiesFrom(Table(readOnly));

            return specification.FetchStrategy == null
                ? query
                : specification.FetchStrategy.IncludePaths.Aggregate(query, (current, path) => current.Include(path));
        }

        public override IReadOnlyCollection<TDto> Get(IEnumerable<int> ids,
            TSpecification specification = default(TSpecification))
        {
            return BaseQuery(specification)
                .Where(dto => ids.Contains(dto.Id))
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="specification"></param>
        /// <returns>Entity</returns>
        public override TDto Get(int id, TSpecification specification = default(TSpecification))
        {
            return BaseQuery(specification)
                .FirstOrDefault(w => w.Id == id);
        }

        /// <summary>
        /// Получить все экземпляры сущности из репозитория
        /// </summary>
        /// <returns>Коллекция всех экземпляров сущности</returns>
        public override IReadOnlyCollection<TDto> GetAll(TSpecification specification = default(TSpecification))
        {
            return BaseQuery(specification)
                .ToList()
                .AsReadOnly();
        }

        public override void Update(TDto entity)
        {
            var entry = Context.Entry(entity);

            try
            {
                if (entry.State == EntityState.Detached)
                {
                    var attachedEntity = Context.Set<TDto>().Find(entity.Id);
                    if (attachedEntity != null)
                    {
                        Context.Entry(attachedEntity).CurrentValues.SetValues(entity);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                // ignore and try the default behavior
            }

            // default
            entry.State = EntityState.Modified;
        }
    }
}
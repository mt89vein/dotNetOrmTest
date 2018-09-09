using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.DataProvider
{
    public abstract class EfRepository<TDomainEntity, TDto, TSpecification> : Repository<TDomainEntity, TDto, TSpecification>
        where TDomainEntity : Entity
        where TDto : class, IDataTransferObject<TDomainEntity>, new()
        where TSpecification : ISpecification<TDto>
    {
        protected readonly ApplicationContext Context;

        private DbSet<TDto> _entities;

        protected virtual IQueryable<TDto> QueryAll => DbSetTable;

        protected DbSet<TDto> DbSetTable => _entities ?? (_entities = Context.Set<TDto>());

        protected EfRepository(ApplicationContext context)
        {
            Context = context;
        }

        protected EntityState UpdateEntryState(EntityEntry entry, bool needUpdate = false)
        {
            if (!needUpdate)
            {
                return EntityState.Unchanged;
            }
            return entry.IsKeySet 
                ? EntityState.Modified 
                : EntityState.Added;
        }

        /// <summary>
        /// Таблица с данными
        /// </summary>
        public IQueryable<TDto> Table => DbSetTable.AsQueryable();

        public override IEnumerable<TDto> GetBySpecification(bool readOnly = true,
            TSpecification specification = default(TSpecification))
        {
            return specification == null
                ? BaseQuery(readOnly: readOnly)
                : BaseQuery(specification, readOnly);
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
        public override IReadOnlyCollection<TDto> Get(TSpecification specification = default(TSpecification))
        {
            return BaseQuery(specification)
                .ToList()
                .AsReadOnly();
        }

        public override void Save(TDto entity, IWorkItemStrategy updateWorkItemStrategy = null)
        {
            var entry = Context.Entry(entity);

            if (entry.IsKeySet)
            {
                if (entry.State == EntityState.Detached)
                {
                    throw new Exception("Попытка сохранить сущность, которая не присоединена к контексту");
                }
                entry.State = EntityState.Modified;
            }
            else
            {
                Context.Add(entry);
            }

            Context.SaveChanges();
        }

        public override void Remove(int id)
        {
            var dto = new TDto {Id = id};

            DbSetTable.Attach(dto);
            DbSetTable.Remove(dto);

            Context.SaveChanges();
        }

        public override void Remove(int[] ids)
        {
            var dtos = ids.Select(id => new TDto {Id = id}).ToList();

            Context.AttachRange(dtos);
            Context.RemoveRange(dtos);

            Context.SaveChanges();
        }

        public override void Remove(TDto entity)
        {
            Remove(entity.Id);
        }

        protected IQueryable<TDto> BaseQuery(ISpecification<TDto> specification = null, bool readOnly = false)
        {
            if (specification == null)
            {
                return Table;
            }

            var query = specification.SatisfyingEntitiesFrom(Table);

            return specification.FetchStrategy == null
                ? query
                : specification.FetchStrategy.IncludePaths.Aggregate(query, (current, path) => current.Include(path));
        }
    }
}
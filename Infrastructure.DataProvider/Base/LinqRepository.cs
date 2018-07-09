using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public abstract class LinqRepository<TEntity, TDto> : Repository<TEntity>
        where TEntity : Entity
        where TDto : class, IDataTransferObject<TEntity>, new()
    {
        private readonly ApplicationContext _context;
        private DbSet<TDto> _entities;

        protected LinqRepository()
        {
            _context = ObjectFactory.Instance.GetObject<ApplicationContext>();
        }

        protected virtual IQueryable<TDto> QueryAll => Table;

        protected DbSet<TDto> Table => _entities ?? (_entities = _context.Set<TDto>());

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public override bool Remove(TEntity entity)
        {
            var dto = QueryAll.SingleOrDefault(d => d.Id == entity.Id);
            if (dto == null)
            {
                return false;
            }

            Table.Remove(dto);
            return true;
        }

        public override IReadOnlyCollection<TEntity> GetMany(IEnumerable<int> ids)
        {
            return QueryAll
                .Where(dto => ids.Contains(dto.Id))
                .Select(dto => dto.Reconstitute())
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public override TEntity Get(int id)
        {
            return QueryAll.FirstOrDefault(w => w.Id == id)?.Reconstitute();
        }

        /// <summary>
        /// Получить все экземпляры сущности из репозитория
        /// </summary>
        /// <returns>Коллекция всех экземпляров сущности</returns>
        public override IReadOnlyCollection<TEntity> GetAll()
        {
            return QueryAll
                .Select(dto => dto.Reconstitute())
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Сохранить экземпляр сущности в репозиторий
        /// </summary>
        /// <param name="entity">Экземпляр сущности для сохранения</param>
        /// <returns>Флаг успешности сохранения</returns>
        public override bool Save(TEntity entity)
        {
            var dto = Table.SingleOrDefault(d => d.Id.Equals(entity.Id));
            var isNew = false;
            if (dto == null)
            {
                dto = new TDto();
                isNew = true;
            }
            dto.Update(entity);

            if (isNew)
                Table.Add(dto);
            //else
                //Table.Update(dto);

            _context.SaveChanges();

            entity.Id = dto.Id;
            return true;
        }
    }
}
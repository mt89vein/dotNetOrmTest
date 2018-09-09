using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider
{
    public abstract class BaseService<TDomainEntity, TDto, TSpecification> : IBaseService<TDomainEntity>
        where TDomainEntity : Entity
        where TSpecification : ISpecification<TDto>
        where TDto : class, IDataTransferObject<TDomainEntity>, new()
    {
        protected readonly ApplicationContext Context;

        protected readonly ICacheService<TDto, TDomainEntity> CacheService;

        protected readonly IRepository<TDomainEntity, TDto, TSpecification> Repository;

        protected BaseService(IRepository<TDomainEntity, TDto, TSpecification> repository, ApplicationContext context,
            ICacheService<TDto, TDomainEntity> cacheService)
        {
            Repository = repository;
            Context = context;
            CacheService = cacheService;
        }

        protected static ISpecification<TDto> OnlyNotDeletedSpecification =>
            new Specification<TDto>(w => w.Deleted == false);

        public virtual void Save(TDomainEntity entity, IWorkItemStrategy workItemStrategy = null)
        {
            var existingEntity = Context.Find<TDto>(entity.Id);

            if (existingEntity != null)
            {
                existingEntity.Update(entity);
                Context.Update(existingEntity);
                Repository.Save(existingEntity, workItemStrategy);
            }
            else
            {
                var dto = new TDto();
                dto.Update(entity);
                Context.Add(dto);
                Repository.Save(dto);
            }
        }

        public virtual void Remove(TDomainEntity entity)
        {
            if (entity != null)
            {
                Remove(entity.Id);
            }
        }

        public virtual void Remove(int id)
        {
            Repository.Remove(id);
        }


        public virtual TDomainEntity Get(int id, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy))
        {
            if (workItemStrategy == null)
            {
                return Repository.Get(id).Reconstitute();
            }

            return !workItemStrategy.CacheResult
                ? Repository.Get(id, ToSpecification(workItemStrategy)).Reconstitute()
                : GetCached(id, workItemStrategy).Reconstitute();
        }

        public virtual IReadOnlyCollection<TDomainEntity> Get(IEnumerable<int> ids,
            IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy))
        {
            if (workItemStrategy == null)
            {
                return Repository.Get(ids).Select(w => w.Reconstitute()).ToList();
            }

            var idsList = ids.ToList();

            if (idsList.Count == 0)
            {
                return new List<TDomainEntity>();
            }

            if (!workItemStrategy.CacheResult)
            {
                return Repository.Get(idsList, ToSpecification(workItemStrategy)).Select(w => w.Reconstitute())
                    .ToList();
            }

            return GetCached(idsList, workItemStrategy).Select(w => w.Reconstitute()).ToList();
        }

        public virtual IReadOnlyCollection<TDomainEntity> Get(IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy))
        {
            if (workItemStrategy == null)
            {
                return Repository.Get().Select(w => w.Reconstitute()).ToList();
            }

            if (!workItemStrategy.CacheResult)
            {
                return Repository.Get(ToSpecification(workItemStrategy)).Select(w => w.Reconstitute())
                    .ToList();
            }

            throw new NotSupportedException();
        }

        protected abstract TSpecification ToSpecification(IWorkItemStrategy workItemStrategy);

        /// <summary>
        /// Читаем с кэша, если там нет, то кэшируем в redis по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        private TDto GetCached(int id, IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy))
        {
            if (workItemStrategy != null)
            {
                return Repository.Get(id, ToSpecification(workItemStrategy));
            }

            var entity = CacheService.Get(id);
            if (entity != null)
            {
                return entity;
            }

            entity = Repository.Get(id);
            CacheService.Set(entity);

            return entity;
        }

        /// <summary>
        /// Если стратегии выборки нет, то используем кэширование
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        private IReadOnlyCollection<TDto> GetCached(ICollection<int> ids,
            IWorkItemStrategy workItemStrategy = default(IWorkItemStrategy))
        {
            if (workItemStrategy != null)
            {
                return Repository.Get(ids, ToSpecification(workItemStrategy)).ToList();
            }

            var entityList = CacheService.Get(ids.ToArray());

            if (entityList?.Count == ids.Count)
            {
                return entityList.ToList();
            }

            entityList = Repository.Get(ids).ToList();
            CacheService.Set(entityList);

            return entityList.ToList();
        }
    }
}
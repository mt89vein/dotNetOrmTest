using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider
{
    public abstract class BaseService<TDomainEntity, TDto, TWorkItemStrategy, TSpecification> : IBaseService<
        TDomainEntity,
        TWorkItemStrategy>
        where TDomainEntity : Entity
        where TWorkItemStrategy : WorkItemStrategy
        where TSpecification : ISpecification<TDto>
        where TDto : IDataTransferObject<TDomainEntity>, new()
    {
        protected readonly ApplicationContext Context;

        protected readonly IRedisService<TDto, TDomainEntity> RedisService;

        protected readonly IRepository<TDomainEntity, TDto, TSpecification> Repository;

        protected BaseService(IRepository<TDomainEntity, TDto, TSpecification> repository, ApplicationContext context,
            IRedisService<TDto, TDomainEntity> redisService)
        {
            Repository = repository;
            Context = context;
            RedisService = redisService;
        }

        protected static ISpecification<TDto> OnlyNotDeletedSpecification =>
            new Specification<TDto>(w => w.Deleted == false);

        public virtual void Insert(TDomainEntity entity)
        {
            var dto = new TDto();
            dto.Update(entity);
            Repository.Add(dto);
        }

        public void Insert(IEnumerable<TDomainEntity> entities)
        {
            entities.ToList().ForEach(Insert);
        }

        public virtual void Update(TDomainEntity entity)
        {
            var dto = new TDto();
            dto.Update(entity);
            Repository.Update(dto);
        }

        public virtual void Update(IEnumerable<TDomainEntity> entities)
        {
            entities.ToList().ForEach(Update);
        }

        public virtual void Delete(TDomainEntity entity)
        {
            if (entity != null)
            {
                Delete(entity.Id);
            }
        }

        public virtual void Delete(int id)
        {
            Repository.Remove(id);
        }

        public virtual void Delete(IEnumerable<TDomainEntity> entities)
        {
            Delete(entities.Select(w => w.Id).ToArray());
        }

        public virtual void Delete(int[] ids)
        {
            if (ids.Length > 0)
            {
                Repository.Remove(ids);
            }
        }

        public TDomainEntity Get(int id, TWorkItemStrategy workItemStrategy = null)
        {
            if (workItemStrategy == null)
            {
                return Repository.Get(id).Reconstitute();
            }

            if (!workItemStrategy.CacheResult)
            {
                return Repository.Get(id, ToSpecification(workItemStrategy)).Reconstitute();
            }

            return GetCached(id, workItemStrategy);
        }

        public IReadOnlyCollection<TDomainEntity> Get(IEnumerable<int> ids, TWorkItemStrategy workItemStrategy = null)
        {
            if (workItemStrategy == null)
            {
                return Repository.Get(ids).Select(w => w.Reconstitute()).ToList();
            }

            var idsList = ids.ToList();

            if (!workItemStrategy.CacheResult)
            {
                return Repository.Get(idsList, ToSpecification(workItemStrategy)).Select(w => w.Reconstitute())
                    .ToList();
            }

            return GetCached(idsList, workItemStrategy);
        }

        public IReadOnlyCollection<TDomainEntity> Get(TWorkItemStrategy workItemStrategy = null)
        {
            if (workItemStrategy == null)
            {
                return Repository.GetAll().Select(w => w.Reconstitute()).ToList();
            }

            if (!workItemStrategy.CacheResult)
            {
                return Repository.GetAll(ToSpecification(workItemStrategy)).Select(w => w.Reconstitute())
                    .ToList();
            }

            throw new NotSupportedException();
        }

        protected abstract TSpecification ToSpecification(TWorkItemStrategy workItemStrategy);

        /// <summary>
        /// Читаем с кэша, если там нет, то кэшируем в redis по ключу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        private TDomainEntity GetCached(int id, TWorkItemStrategy workItemStrategy = null)
        {
            if (workItemStrategy != null)
            {
                return Repository.Get(id, ToSpecification(workItemStrategy)).Reconstitute();
            }

            var entity = RedisService.Get(id);
            if (entity != null)
            {
                return entity.Reconstitute();
            }

            entity = Repository.Get(id);
            RedisService.Set(entity);

            return entity.Reconstitute();
        }

        /// <summary>
        /// Если стратегии выборки нет, то используем кэширование
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="workItemStrategy"></param>
        /// <returns></returns>
        private IReadOnlyCollection<TDomainEntity> GetCached(ICollection<int> ids, TWorkItemStrategy workItemStrategy = null)
        {
            if (workItemStrategy != null)
            {
                return Repository.Get(ids, ToSpecification(workItemStrategy)).Select(w => w.Reconstitute()).ToList();
            }
            
            var entityList = RedisService.Get(ids.ToArray());

            if (entityList?.Count == ids.Count)
            {
                return entityList.Select(w => w.Reconstitute()).ToList();
            }

            entityList = Repository.Get(ids).ToList();
            RedisService.Set(entityList);

            return entityList.Select(w => w.Reconstitute()).ToList();
        }
    }
}
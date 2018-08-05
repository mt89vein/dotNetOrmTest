using System;
using System.Collections.Generic;
using Infrastructure.DomainBase;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.DataProvider.Caching
{
    public class RedisService<TDto, TDomainEntity> : IRedisService<TDto, TDomainEntity>
        where TDto : IDataTransferObject<TDomainEntity>
        where TDomainEntity : IEntity
    {
        private readonly Lazy<ConnectionMultiplexer> _connection =
            new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));

        public IDatabase RedisDb => _connection.Value.GetDatabase();

        public bool HasKey(int id)
        {
            return RedisDb.KeyExists(BuildKey(id));
        }

        public bool HasKey(TDto entity)
        {
            return entity != null && RedisDb.KeyExists(BuildKey(entity.Id));
        }

        public TDto Get(int id)
        {
            if (!HasKey(id))
            {
                return default(TDto);
            }

            var serialized = RedisDb.StringGet(BuildKey(id));
            return JsonConvert.DeserializeObject<TDto>(serialized);
        }

        public IReadOnlyCollection<TDto> Get(int[] ids)
        {
            var items = new List<TDto>();
            foreach (var id in ids)
            {
                var item = Get(id);
                if (item == null)
                {
                    return null;
                }
                items.Add(item);
            }
            return items;
        }

        public void Set(TDto entity)
        {
            RedisDb.StringSetAsync(BuildKey(entity.Id),
                JsonConvert.SerializeObject((object) entity,
                    new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
        }

        public void Set(IEnumerable<TDto> entities)
        {
            foreach (var entity in entities)
            {
                Set(entity);
            }
        }

        public void Delete(int id)
        {
            RedisDb.KeyDelete(BuildKey(id));
        }

        public void Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        public void Delete(TDto entity)
        {
            if (entity != null)
            {
                Delete(entity.Id);
            }
        }

        public void Delete(IEnumerable<TDto> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        private string BuildKey(int id)
        {
            return $"{typeof(TDto).Name}:{id}";
        }
    }
}
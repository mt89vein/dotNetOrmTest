using System;
using System.Collections.Generic;
using System.Linq;
using Domain.FetchStrategies;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
	public static class WorkItemStrategyHelper
	{
		private static Specification<OtherDocumentDto> AsSpecification(this OtherDocumentWorkItemStrategy strategy)
		{
			var specification = new Specification<OtherDocumentDto>();

			if (strategy.WithAttachments)
			{
				specification.FetchStrategy.Include(w => w.DocumentDto.AttachmentDtos);
			}

			if (strategy.WithItems)
			{
				specification.FetchStrategy.Include(w => w.OtherDocumentItemDtos);
			}

			if (strategy.WithPayments)
			{
				specification.FetchStrategy.Include(w => w.OtherDocumentPaymentDtos);
			}

			if (!strategy.WithDeleted)
			{
				specification.Predicate = w => !w.Deleted;
			}

			return specification;
		}

		private static Specification<DocumentDto> AsSpecification(this DocumentWorkItemStrategy strategy)
		{
			var specification = new Specification<DocumentDto>();

			if (strategy.WithAttachments)
			{
				specification.FetchStrategy.Include(w => w.AttachmentDtos);
			}

			if (!strategy.WithDeleted)
			{
				specification.Predicate = w => !w.Deleted;
			}

			specification.FetchStrategy.Include(w => w.OtherDocumentDto);

			return specification;
		}

		public static Specification<TDto> AsSpecification<TEntity, TDto, TWorkItemStrategy>(this TWorkItemStrategy strategy)
			where TEntity : Entity
			where TDto : class, IDataTransferObject<TEntity, TWorkItemStrategy>, new()
			where TWorkItemStrategy : WorkItemStrategy
		{
			switch (strategy)
			{
				case OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy:
					return (Specification<TDto>)(object)otherDocumentWorkItemStrategy.AsSpecification();
				case DocumentWorkItemStrategy documentWorkItemStrategy:
					return (Specification<TDto>)(object)documentWorkItemStrategy.AsSpecification();
				default:
				{
					var spec = new Specification<TDto>();
					if (!strategy.WithDeleted)
					{
						spec.Predicate = w => !w.Deleted;
					}

					return spec;
				}
			}
		}
	}

	public abstract class LinqRepository<TEntity, TDto, TWorkItemStrategy> : Repository<TEntity, TWorkItemStrategy>
		where TEntity : Entity
		where TDto : class, IDataTransferObject<TEntity, TWorkItemStrategy>, new()
		where TWorkItemStrategy : WorkItemStrategy
	{
		protected readonly ApplicationContext Context;
		private DbSet<TDto> _entities;

		protected LinqRepository(ApplicationContext context)
		{
			Context = context;
		}

		protected virtual IQueryable<TDto> QueryAll => Table;

		protected DbSet<TDto> Table => _entities ?? (_entities = Context.Set<TDto>());

		protected abstract Specification<TDto> ToSpecification(TWorkItemStrategy workItemStrategy);

		/// <summary>
		///     Remove entity
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

		protected IQueryable<TDto> BaseQuery(ISpecification<TDto> specification)
		{
			var query = QueryAll;

			query = specification.SatisfyingEntitiesFrom(query);

			return specification.FetchStrategy == null
				? query
				: specification.FetchStrategy.IncludePaths.Aggregate(query, (current, path) => current.Include(path));
		}

		public override IReadOnlyCollection<TEntity> GetMany(IEnumerable<int> ids, TWorkItemStrategy workItemStrategy = null)
		{
			var query = QueryAll;
			if (workItemStrategy != null)
			{
				query = BaseQuery(ToSpecification(workItemStrategy));
			}

			return query
				.Where(dto => ids.Contains(dto.Id))
				.Select(dto => dto.Reconstitute(workItemStrategy))
				.ToList()
				.AsReadOnly();
		}

		/// <summary>
		///     Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <param name="workItemStrategy"></param>
		/// <returns>Entity</returns>
		public override TEntity Get(int id, TWorkItemStrategy workItemStrategy = null)
		{
			//var query = workItemStrategy != null
			//	? BaseQuery(ToSpecification(workItemStrategy))
			//	: QueryAll;

			var query = workItemStrategy != null
				? BaseQuery(workItemStrategy.AsSpecification<TEntity, TDto, TWorkItemStrategy>())
				: QueryAll;

			return query
				.FirstOrDefault(w => w.Id == id)
				?.Reconstitute(workItemStrategy);
		}

		/// <summary>
		///     Получить все экземпляры сущности из репозитория
		/// </summary>
		/// <returns>Коллекция всех экземпляров сущности</returns>
		public override IReadOnlyCollection<TEntity> GetAll(TWorkItemStrategy workItemStrategy = null)
		{
			var query = workItemStrategy != null
				? BaseQuery(workItemStrategy.AsSpecification<TEntity, TDto, TWorkItemStrategy>())
				: QueryAll;

			return query
				.Select(dto => dto.Reconstitute(workItemStrategy))
				.ToList()
				.AsReadOnly();
		}

		/// <summary>
		///     Сохранить экземпляр сущности в репозиторий
		/// </summary>
		/// <param name="entity">Экземпляр сущности для сохранения</param>
		/// <param name="workItemStrategy"></param>
		/// <returns>Флаг успешности сохранения</returns>
		public override bool Save(TEntity entity, TWorkItemStrategy workItemStrategy = null)
		{
			var dto = Table.SingleOrDefault(d => d.Id.Equals(entity.Id));
			var isNew = false;
			if (dto == null)
			{
				dto = new TDto();
				isNew = true;
			}

			dto.Update(entity, workItemStrategy);
			Update(dto);

			if (isNew)
			{
				Table.Add(dto);
			}

			Context.SaveChanges();

			entity.Id = dto.Id;
			return true;
		}

		protected void Update(TDto entity)
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
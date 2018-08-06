using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentItemRepository :
        LinqRepository<OtherDocumentItem, OtherDocumentItemDto, ISpecification<OtherDocumentItemDto>>,
        IOtherDocumentItemRepository
    {
        public OtherDocumentItemRepository(ApplicationContext context) : base(context)
        {
        }

        public IReadOnlyCollection<OtherDocumentItemDto> GetOtherDocumentItemsByDocumentId(int documentId,
            ISpecification<OtherDocumentItemDto> specification = null)
        {
            return BaseQuery(specification)
                .Where(w => w.OtherDocumentId == documentId)
                .ToList();
        }

        public override void Update(OtherDocumentItemDto entity, IWorkItemStrategy updateWorkItemStrategy = null)
        {
            if (!(updateWorkItemStrategy is OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy))
            {
                base.Update(entity);
                return;
            }

            var entry = Context.Entry(entity);

            try
            {
                if (entry.State == EntityState.Detached)
                {
                    Context.Attach(entity);
                }

                if (otherDocumentItemWorkItemStrategy.WithNestedItems)
                {
                    foreach (var nestedItemDto in entity.NestedItemDtos)
                    {
                        Context.Entry(nestedItemDto).State = EntityState.Modified;

                        if (otherDocumentItemWorkItemStrategy.NestedItemWorkItemStrategy.WithOneMoreNestedItems)
                        {
                            foreach (var oneMoreNestedItemDto in nestedItemDto.OneMoreNestedItemDtos)
                            {
                                Context.Entry(oneMoreNestedItemDto).State = EntityState.Modified;
                            }
                        }
                    }
                    return;
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
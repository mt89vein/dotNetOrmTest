using System;
using System.Linq;
using Domain;
using Domain.FetchStrategies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentRepository : EfRepository<OtherDocument, OtherDocumentDto,
        ISpecification<OtherDocumentDto>>, IOtherDocumentRepository
    {
        public OtherDocumentRepository(ApplicationContext context)
            : base(context)
        {
        }

        public override void Save(OtherDocumentDto entity, IWorkItemStrategy updateWorkItemStrategy = null)
        {
            if (!(updateWorkItemStrategy is OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy))
            {
                base.Save(entity, updateWorkItemStrategy);
                return;
            }

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

            if (!otherDocumentWorkItemStrategy.WithAttachments)
            {
                Context.Entry(entity.DocumentDto.AttachmentLinkDtos).State = EntityState.Detached;
            }

            if (!otherDocumentWorkItemStrategy.WithPayments)
            {
                Context.Entry(entity.OtherDocumentPaymentDtos).State = EntityState.Detached;
            }


            if (!otherDocumentWorkItemStrategy.WithItems)
            {
                Context.Entry(entity.OtherDocumentItemDtos).State = EntityState.Detached;
            }
            if (!otherDocumentWorkItemStrategy.OtherDocumentItemWorkItemStrategy.WithNestedItems)
            {
                foreach (var entityOtherDocumentItemDto in entity.OtherDocumentItemDtos)
                {
                    Context.Entry(entityOtherDocumentItemDto.NestedItemDtos).State = EntityState.Detached;
                }
            }
            else if (!otherDocumentWorkItemStrategy.OtherDocumentItemWorkItemStrategy.NestedItemWorkItemStrategy
                .WithOneMoreNestedItems)
            {
                foreach (var nestedItemDto in entity.OtherDocumentItemDtos.SelectMany(w => w.NestedItemDtos))
                {
                    Context.Entry(nestedItemDto.OneMoreNestedItemDtos).State = EntityState.Unchanged;
                }
            }

            Context.SaveChanges();
        }
    }
}
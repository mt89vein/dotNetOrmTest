using System;
using Domain;
using Domain.FetchStrategies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentRepository : LinqRepository<OtherDocument, OtherDocumentDto,
            ISpecification<OtherDocumentDto>>,
        IOtherDocumentRepository
    {
        public OtherDocumentRepository(ApplicationContext context)
            : base(context)
        {
        }

        public override void Update(OtherDocumentDto entity, IWorkItemStrategy updateWorkItemStrategy = null)
        {
            if (!(updateWorkItemStrategy is OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy))
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
                if (otherDocumentWorkItemStrategy.WithAttachments)
                {
                    foreach (var attachmentLinkDto in entity.DocumentDto.AttachmentLinkDtos)
                    {
                        Context.Entry(attachmentLinkDto).State = EntityState.Modified;
                    }
                }
                if (otherDocumentWorkItemStrategy.WithPayments)
                {
                    foreach (var paymentDto in entity.OtherDocumentPaymentDtos)
                    {
                        Context.Entry(paymentDto).State = EntityState.Modified;
                    }
                }
                if (otherDocumentWorkItemStrategy.WithItems)
                {
                    foreach (var itemDto in entity.OtherDocumentItemDtos)
                    {
                        Context.Entry(itemDto).State = EntityState.Modified;
                        if (otherDocumentWorkItemStrategy.OtherDocumentItemWorkItemStrategy.WithNestedItems)
                        {
                            foreach (var nestedItemDto in itemDto.NestedItemDtos)
                            {
                                Context.Entry(nestedItemDto).State = EntityState.Modified;

                                foreach (var oneMoreNestedItemDto in nestedItemDto.OneMoreNestedItemDtos)
                                {
                                    Context.Entry(oneMoreNestedItemDto).State = EntityState.Modified;
                                }
                            }
                        }
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
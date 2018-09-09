using System;
using Domain;
using Domain.FetchStrategies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class DocumentRepository : EfRepository<Document, DocumentDto, ISpecification<DocumentDto>>,
        IDocumentRepository
    {
        public DocumentRepository(ApplicationContext context) : base(context)
        {
        }

        public override void Save(DocumentDto entity, IWorkItemStrategy updateWorkItemStrategy = null)
        {
            if (!(updateWorkItemStrategy is DocumentWorkItemStrategy documentWorkItemStrategy))
            {
                base.Save(entity);
                return;
            }

            var entry = Context.Entry(entity);


            try
            {
                if (documentWorkItemStrategy.WithAttachments)
                {
                    if (entry.State == EntityState.Detached)
                    {
                        Context.Attach(entity);
                    }
                    foreach (var attachmentLinkDto in entity.AttachmentLinkDtos)
                    {
                        Context.Entry(attachmentLinkDto).State = EntityState.Modified;
                    }

                    //var attachedEntity = DbSetTable.Include(w => w.AttachmentLinkDtos).FirstOrDefault(w => w.Id == entity.Id);
                    //    if (attachedEntity != null)
                    //    {
                    //        Context.Entry(attachedEntity).CurrentValues.SetValues(entity);
                    //        var attachmentLinks = attachedEntity.AttachmentLinkDtos.ToList();
                    //        foreach (var attachmentLink in attachmentLinks)
                    //        {
                    //            var attachment = entity.AttachmentLinkDtos.SingleOrDefault(i => i.Id == attachmentLink.Id);
                    //            if (attachment != null)
                    //                Context.Entry(attachmentLink).CurrentValues.SetValues(attachment);
                    //            else
                    //                Context.Remove(attachmentLink);
                    //        }
                    //        return;
                    //    }
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
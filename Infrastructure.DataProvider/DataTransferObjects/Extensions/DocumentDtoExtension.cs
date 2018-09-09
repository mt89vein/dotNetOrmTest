using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class DocumentDto : IDataTransferObject<Document>
    {
        public Document Reconstitute()
        {
            switch (DocumentType)
            {
                case DocumentType.OtherDocument:
                    return OtherDocumentDto?.Reconstitute();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update(Document entity)
        {
            switch (entity)
            {
                case OtherDocument otherDocument:
                {
                    if (OtherDocumentDto == null)
                    {
                        OtherDocumentDto = new OtherDocumentDto();
                    }
                    OtherDocumentDto.Update(otherDocument);
                    return;
                }
                // other inherited documents...
            }
        }
    }

    public static class DocumentDtoReconstituteExtensions
    {
        public static void UpdateFrom(this DocumentDto dto, Document entity)
        {
            var newAttachmentLinks = new List<AttachmentLinkDto>();

            foreach (var entityAttachment in entity.Attachments)
            {
                var link = dto.AttachmentLinkDtos.SingleOrDefault(w => w.AttachmentId == entityAttachment.Id);
                if (link != null)
                {
                    newAttachmentLinks.Add(link);
                }
                else
                {
                    newAttachmentLinks.Add(new AttachmentLinkDto
                    {
                        AttachmentId = entityAttachment.Id,
                        DocumentId = entity.Id
                    });
                }
            }

            dto.AttachmentLinkDtos = newAttachmentLinks;
            dto.Name = entity.Name;
            dto.Deleted = entity.Deleted;
        }
    }
}
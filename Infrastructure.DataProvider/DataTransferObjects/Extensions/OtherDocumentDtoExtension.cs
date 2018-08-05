using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentDto : IDataTransferObject<OtherDocument>
    {
        public OtherDocument Reconstitute()
        {
            var attachments = DocumentDto.AttachmentDtos?.Select(w => w.Reconstitute()).ToList() ??
                              new List<Attachment>();

            return new OtherDocument(Id, DocumentDto.Name, TestName, attachments, Deleted);
        }

        public void Update(OtherDocument entity)
        {
            if (DocumentDto == null)
            {
                DocumentDto = new DocumentDto
                {
                    Id = Id
                };
            }

            DocumentDto.Update(entity);
            DocumentDto.DocumentType = DocumentType.OtherDocument;
            TestName = entity.TestName;
        }
    }
}
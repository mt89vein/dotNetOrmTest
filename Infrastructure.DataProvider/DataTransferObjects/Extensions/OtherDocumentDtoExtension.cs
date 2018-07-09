using Domain;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentDto : IDataTransferObject<OtherDocument>
    {
        public OtherDocument Reconstitute()
        {
            return new OtherDocument(Id, ReconstitutePublicationEvent(), DocumentDto.Name, TestName);
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

        public PublicationEvent ReconstitutePublicationEvent()
        {
            return new PublicationEvent(PublicationEvent.UserId, PublicationEvent.Date);
        }
    }
}
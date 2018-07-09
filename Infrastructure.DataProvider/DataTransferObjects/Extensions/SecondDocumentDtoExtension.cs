using Domain;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class SecondDocumentDto : IDataTransferObject<SecondDocument>
    {
        public SecondDocument Reconstitute()
        {
            return new SecondDocument(Id, DocumentDto.Name, DocumentSigner, CreatedAt);
        }

        public void Update(SecondDocument entity)
        {
            if (DocumentDto == null)
            {
                DocumentDto = new DocumentDto
                {
                    Id = Id
                };
            }
            DocumentDto.Update(entity);
            DocumentDto.DocumentType = DocumentType.SecondDocument;
            CreatedAt = entity.CreatedAt;
            DocumentSigner = DocumentSigner;
        }
    }
}
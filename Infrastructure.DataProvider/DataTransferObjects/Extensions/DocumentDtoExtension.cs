using System;
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
                    return OtherDocumentDto.Reconstitute();
                case DocumentType.SecondDocument:
                    return SecondDocumentDto.Reconstitute();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update(Document entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            if (entity is OtherDocument otherDocument)
            {
                if (OtherDocumentDto == null)
                {
                    OtherDocumentDto = new OtherDocumentDto();
                }
                OtherDocumentDto.Update(otherDocument);
                return;
            }

            if (entity is SecondDocument secondDocument)
            {
                if (SecondDocumentDto == null)
                {
                    SecondDocumentDto = new SecondDocumentDto();
                }
                SecondDocumentDto.Update(secondDocument);
            }
        }
    }
}
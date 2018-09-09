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
            var attachments = DocumentDto.AttachmentLinkDtos
                ?.Select(w => w.AttachmentDto.Reconstitute())
                .ToList() ?? new List<Attachment>();

            var items = OtherDocumentItemDtos
                ?.Select(w => w.Reconstitute())
                .ToList() ?? new List<OtherDocumentItem>();

            var payments = OtherDocumentPaymentDtos
                ?.Select(w => w.Reconstitute())
                .ToList() ?? new List<OtherDocumentPayment>();

            return new OtherDocument(Id, DocumentDto.Name, TestName, attachments, Deleted, payments, items);
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
            DocumentDto.UpdateFrom(entity);
            DocumentDto.DocumentType = DocumentType.OtherDocument;
            OtherDocumentPaymentDtos = entity.Payments.Select(UpdatePayment).ToList();
            TestName = entity.TestName;
            OtherDocumentItemDtos = entity.Items.Select(OtherDocumentItemExtension.UpdateItem).ToList();

            OtherDocumentPaymentDto UpdatePayment(OtherDocumentPayment payment)
            {
                return new OtherDocumentPaymentDto
                {
                    Id = payment.Id,
                    Deleted = payment.Deleted,
                    OtherDocumentId = payment.OtherDocumentId,
                    Total = payment.Total
                };
            }
        }
    }
}
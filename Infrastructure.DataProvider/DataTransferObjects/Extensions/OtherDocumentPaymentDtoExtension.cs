using Domain;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentPaymentDto : IDataTransferObject<OtherDocumentPayment>
    {
        public OtherDocumentPayment Reconstitute()
        {
            return new OtherDocumentPayment(OtherDocumentId, Total, Deleted, Id);
        }

        public void Update(OtherDocumentPayment entity)
        {
            Id = entity.Id;
            Total = entity.Total;
            OtherDocumentId = entity.OtherDocumentId;
        }
    }
}
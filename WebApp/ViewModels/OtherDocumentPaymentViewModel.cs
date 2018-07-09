using Domain;

namespace OrmTest.ViewModels
{
    public class OtherDocumentPaymentViewModel
    {
        public OtherDocumentPaymentViewModel()
        {
        }

        public OtherDocumentPaymentViewModel(OtherDocumentPayment payment)
        {
            Id = payment.Id;
            Total = payment.Total;
            OtherDocumentId = payment.OtherDocumentId;
        }

        public int? Id { get; set; }

        public int OtherDocumentId { get; set; }

        public string Total { get; set; }

        public OtherDocumentPayment GetModel()
        {
            return Id.HasValue
                ? new OtherDocumentPayment(Id.Value, Total, OtherDocumentId)
                : new OtherDocumentPayment(OtherDocumentId, Total);
        }
    }
}
using Domain;

namespace WebApp.ViewModels
{
    public class OtherDocumentPaymentEditViewModel
    {
        public OtherDocumentPaymentEditViewModel(OtherDocumentPayment payment)
        {
            Id = payment.Id;
            Deleted = payment.Deleted;
            Total = payment.Total;
            OtherDocumentId = payment.OtherDocumentId;
        }

        public OtherDocumentPaymentEditViewModel()
        {
            Deleted = false;
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public string Total { get; set; }

        public int OtherDocumentId { get; set; }

        public OtherDocumentPayment GetModel()
        {
            return new OtherDocumentPayment(OtherDocumentId, Total, Deleted, Id);
        }
    }
}
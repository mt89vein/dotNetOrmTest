using Infrastructure.DomainBase;

namespace Domain
{
    public class OtherDocumentPayment : Entity
    {
        public OtherDocumentPayment(int otherDocumentId, string total)
        {
            Total = total;
            OtherDocumentId = otherDocumentId;
        }

        public OtherDocumentPayment(int id, string total, int otherDocumentId)
            : base(id)
        {
            Id = id;
            Total = total;
            OtherDocumentId = otherDocumentId;
        }

        public static IOtherDocumentPaymentRepository Repository => ObjectFactory.Instance
            .GetObject<IOtherDocumentPaymentRepository>();

        public string Total { get; }

        public int OtherDocumentId { get; }
    }
}
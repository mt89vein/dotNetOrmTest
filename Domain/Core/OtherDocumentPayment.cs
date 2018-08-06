using Infrastructure.DomainBase;

namespace Domain
{
    public class OtherDocumentPayment : Entity
    {
        public OtherDocumentPayment(int otherDocumentId, string total, bool deleted, int id) : base(id, deleted)
        {
            Total = total;
            OtherDocumentId = otherDocumentId;
        }

        public string Total { get; }

        public int OtherDocumentId { get; }
    }
}
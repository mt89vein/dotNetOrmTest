using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
    public interface IOtherDocumentPaymentRepository : IRepository<OtherDocumentPayment>
    {
        IReadOnlyCollection<OtherDocumentPayment> GetOtherDocumentPaymentsByDocumentId(int documentId);
    }
}
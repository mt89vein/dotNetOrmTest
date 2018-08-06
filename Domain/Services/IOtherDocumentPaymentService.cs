using System.Collections.Generic;
using Domain.FetchStrategies;

namespace Domain.Services
{
    public interface IOtherDocumentPaymentService : IBaseService<OtherDocumentPayment>
    {
        IReadOnlyCollection<OtherDocumentPayment> GetByOtherDocumentId(int otherDocumentId, OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy);
    }
}
using System.Collections.Generic;
using Domain.FetchStrategies;
using Infrastructure.DomainBase;

namespace Domain
{
	public interface
		IOtherDocumentPaymentRepository : IRepository<OtherDocumentPayment, OtherDocumentPaymentWorkItemStrategy>
	{
		IReadOnlyCollection<OtherDocumentPayment> GetOtherDocumentPaymentsByDocumentId(int documentId,
			OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy = null);
	}
}
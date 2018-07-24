using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class OtherDocumentPaymentWorkItemStrategy : WorkItemStrategy
	{
		public OtherDocumentPaymentWorkItemStrategy(bool withDeleted = false)
			: base(withDeleted)
		{
		}
	}
}
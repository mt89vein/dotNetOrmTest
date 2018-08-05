using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class OtherDocumentPaymentWorkItemStrategy : WorkItemStrategy
	{
		public OtherDocumentPaymentWorkItemStrategy(bool withDeleted = false, bool readOnly = false, bool cacheResult = false)
			: base(withDeleted, readOnly, cacheResult)
		{
		}
	}
}
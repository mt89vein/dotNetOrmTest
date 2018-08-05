using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class OtherDocumentItemWorkItemStrategy : WorkItemStrategy
	{
		public OtherDocumentItemWorkItemStrategy(bool withDeleted = false, bool readOnly = false, bool cacheResult = false)
			: base(withDeleted, readOnly, cacheResult)
		{
		}
	}
}
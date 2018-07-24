using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class OtherDocumentItemWorkItemStrategy : WorkItemStrategy
	{
		public OtherDocumentItemWorkItemStrategy(bool withDeleted = false)
			: base(withDeleted)
		{
		}
	}
}
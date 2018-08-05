using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class AttachmentWorkItemStrategy : WorkItemStrategy
	{
		protected AttachmentWorkItemStrategy(bool withDeleted = false, bool readOnly = false, bool cacheResult = false) : base(withDeleted, readOnly, cacheResult)
		{
		}
	}
}
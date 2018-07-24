using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class AttachmentWorkItemStrategy : WorkItemStrategy
	{
		protected AttachmentWorkItemStrategy(bool withDeleted = false) : base(withDeleted)
		{
		}
	}
}
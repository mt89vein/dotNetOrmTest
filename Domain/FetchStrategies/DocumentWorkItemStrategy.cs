using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class DocumentWorkItemStrategy : WorkItemStrategy
	{
		protected DocumentWorkItemStrategy(bool withDeleted = false, bool withAttachments = true, bool readOnly = false, bool cacheResult = false) 
			: base(withDeleted, readOnly, cacheResult)
		{
			WithAttachments = withAttachments;
		}

		public bool WithAttachments { get; }
	}
}
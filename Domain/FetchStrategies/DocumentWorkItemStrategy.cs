using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class DocumentWorkItemStrategy : WorkItemStrategy
	{
		protected DocumentWorkItemStrategy(bool withDeleted = false, bool withAttachments = true) 
			: base(withDeleted)
		{
			WithAttachments = withAttachments;
		}

		public bool WithAttachments { get; }
	}
}
namespace Domain.FetchStrategies
{
	public class OtherDocumentWorkItemStrategy : DocumentWorkItemStrategy
	{
		public OtherDocumentWorkItemStrategy(bool withDeleted = false, bool withAttachments = true, bool withPayments = true, bool withItems = true)
			: base(withDeleted, withAttachments)
		{
			WithPayments = withPayments;
			WithItems = withItems;
		}

		public bool WithPayments { get; }

		public bool WithItems { get; }
	}
}
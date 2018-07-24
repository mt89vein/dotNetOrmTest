namespace Infrastructure.DomainBase
{
	public class WorkItemStrategy
	{
		public WorkItemStrategy(bool withDeleted)
		{
			WithDeleted = withDeleted;
		}

		public bool WithDeleted { get; }
	}
}
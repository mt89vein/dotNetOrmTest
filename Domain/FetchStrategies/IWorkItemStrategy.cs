namespace Domain
{
	public interface IWorkItemStrategy
	{
		bool WithDeleted { get; }

        bool CacheResult { get; }
	}
}
namespace Domain
{
	public interface IWorkItemStrategy
	{
		bool WithDeleted { get; }

        bool ReadOnly { get; }

        bool CacheResult { get; }
	}
}
namespace Domain
{
	public class WorkItemStrategy
	{
		public WorkItemStrategy(bool withDeleted, bool readOnly, bool cacheResult = false)
		{
		    WithDeleted = withDeleted;
		    ReadOnly = readOnly;
		    CacheResult = cacheResult;
		}

		public bool WithDeleted { get; }

        public bool ReadOnly { get; }

        public bool CacheResult { get; }
	}
}
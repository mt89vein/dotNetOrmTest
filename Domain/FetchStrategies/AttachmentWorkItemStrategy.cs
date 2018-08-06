using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class AttachmentWorkItemStrategy : IWorkItemStrategy
	{
		protected AttachmentWorkItemStrategy(bool withDeleted = false, bool readOnly = false, bool cacheResult = false)
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
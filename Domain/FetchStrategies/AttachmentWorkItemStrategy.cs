using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class AttachmentWorkItemStrategy : IWorkItemStrategy
	{
		protected AttachmentWorkItemStrategy(bool withDeleted = false, bool cacheResult = false)
		{
		    WithDeleted = withDeleted;
		    CacheResult = cacheResult;
		}

        public bool WithDeleted { get; }

        public bool CacheResult { get; }
    }
}
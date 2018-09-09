namespace Domain.FetchStrategies
{
    public class DocumentWorkItemStrategy : IWorkItemStrategy
    {
        public DocumentWorkItemStrategy(bool withDeleted = false, bool withAttachments = true, bool cacheResult = false)
        {
            WithDeleted = withDeleted;
            WithAttachments = withAttachments;
            CacheResult = cacheResult;
        }

        public bool WithAttachments { get; }

        public bool WithDeleted { get; }

        public bool CacheResult { get; }
    }
}
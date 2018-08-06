namespace Domain.FetchStrategies
{
    public class DocumentWorkItemStrategy : IWorkItemStrategy
    {
        protected DocumentWorkItemStrategy(bool withDeleted = false, bool withAttachments = true, bool readOnly = false,
            bool cacheResult = false)
        {
            WithDeleted = withDeleted;
            WithAttachments = withAttachments;
            ReadOnly = readOnly;
            CacheResult = cacheResult;
        }

        public bool WithAttachments { get; }

        public bool WithDeleted { get; }

        public bool ReadOnly { get; }

        public bool CacheResult { get; }
    }
}
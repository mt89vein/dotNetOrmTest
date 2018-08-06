using System;
using Domain;

namespace Domain.FetchStrategies
{
    public class OtherDocumentItemWorkItemStrategy : IWorkItemStrategy
    {
        public OtherDocumentItemWorkItemStrategy(bool withNestedItems = true,
            NestedItemWorkItemStrategy nestedItemWorkItemStrategy = null, bool withDeleted = false,
            bool readOnly = false, bool cacheResult = false)
        {
            WithNestedItems = withNestedItems;
            if (withNestedItems && nestedItemWorkItemStrategy == null)
            {
                throw new ArgumentNullException(nameof(nestedItemWorkItemStrategy));
            }
            NestedItemWorkItemStrategy = nestedItemWorkItemStrategy;
            WithDeleted = withDeleted;
            ReadOnly = readOnly;
            CacheResult = cacheResult;
        }

        public bool WithNestedItems { get; }

        public NestedItemWorkItemStrategy NestedItemWorkItemStrategy { get; }

        public bool WithDeleted { get; }

        public bool ReadOnly { get; }

        public bool CacheResult { get; }
    }
}

public class NestedItemWorkItemStrategy : IWorkItemStrategy
{
    public NestedItemWorkItemStrategy(bool withOneMoreNestedItems = true, bool withDeleted = false,
        bool readOnly = false, bool cacheResult = false)
    {
        WithOneMoreNestedItems = withOneMoreNestedItems;
        WithDeleted = withDeleted;
        ReadOnly = readOnly;
        CacheResult = cacheResult;
    }

    public bool WithOneMoreNestedItems { get; }

    public bool WithDeleted { get; }

    public bool ReadOnly { get; }

    public bool CacheResult { get; }
}
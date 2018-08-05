using System;
using System.Linq.Expressions;

namespace Infrastructure.DataProvider.Helpers
{
    public static class RepositoryHelper
    {
        public static IFetchStrategy<T> BuildFetchStrategy<T>(params string[] includePaths)
        {
            var fetchStrategy = new GenericFetchStrategy<T>();
            foreach (var path in includePaths) fetchStrategy.Include(path);
            return fetchStrategy;
        }

        public static IFetchStrategy<T> BuildFetchStrategy<T>(params Expression<Func<T, object>>[] includePaths)
        {
            var fetchStrategy = new GenericFetchStrategy<T>();
            foreach (var path in includePaths) fetchStrategy.Include(path);
            return fetchStrategy;
        }
    }
}
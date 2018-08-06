using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Helpers
{
    public static class RepositoryHelper
    {
        public static IFetchStrategy<DbSet<T>> BuildFetchStrategy<T>(params string[] includePaths) where T : class
        {
            var fetchStrategy = new GenericFetchStrategy<T>();
            foreach (var path in includePaths) fetchStrategy.Add(path);
            return fetchStrategy;
        }

        public static IFetchStrategy<DbSet<T>> BuildFetchStrategy<T>(params Expression<Func<DbSet<T>, object>>[] includePaths) where T : class
        {
            var fetchStrategy = new GenericFetchStrategy<T>();
            foreach (var path in includePaths) fetchStrategy.Add(path);
            return fetchStrategy;
        }
    }
}
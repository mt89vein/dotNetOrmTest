using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public abstract class AbstractFetchStrategy<T> : IFetchStrategy<DbSet<T>>
        where T : class
    {
        public abstract IEnumerable<string> IncludePaths { get; }

        public abstract IFetchStrategy<DbSet<T>> Add(Expression<Func<DbSet<T>, object>> path);

        public abstract IFetchStrategy<DbSet<T>> Add(string path);

        public override string ToString()
        {
            return string.Format("Type: {0} Includes: {1}",
                GetType().Name,
                string.Join(",", IncludePaths)
            );
        }
    }
}
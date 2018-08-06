using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.DataProvider
{
    public interface IFetchStrategy<T>
    {
        IEnumerable<string> IncludePaths { get; }

        IFetchStrategy<T> Add(Expression<Func<T, object>> path);

        IFetchStrategy<T> Add(string path);
    }
}
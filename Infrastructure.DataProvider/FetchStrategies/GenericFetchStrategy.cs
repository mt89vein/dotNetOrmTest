using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public class GenericFetchStrategy<T> : AbstractFetchStrategy<T>
        where T : class
    {
        private readonly IList<string> _properties;

        public GenericFetchStrategy()
        {
            _properties = new List<string>();
        }

        public override IEnumerable<string> IncludePaths => _properties;

        public override IFetchStrategy<DbSet<T>> Add(Expression<Func<DbSet<T>, object>> path)
        {
            return Add(path.ToIncludeString());
        }

        public override IFetchStrategy<DbSet<T>> Add(string path)
        {
            _properties.Add(path);
            return this;
        }
    }
}
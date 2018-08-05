using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.DataProvider
{
    public class GenericFetchStrategy<T> : AbstractFetchStrategy<T>
    {
        private readonly IList<string> _properties;

        public GenericFetchStrategy()
        {
            _properties = new List<string>();
        }

        public override IEnumerable<string> IncludePaths => _properties;

        public override IFetchStrategy<T> Include(Expression<Func<T, object>> path)
        {
            return Include(path.ToIncludeString());
        }

        public override IFetchStrategy<T> Include(string path)
        {
            _properties.Add(path);
            return this;
        }
    }
}
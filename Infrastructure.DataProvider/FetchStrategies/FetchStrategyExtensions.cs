using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Infrastructure.DataProvider.Helpers;

namespace Infrastructure.DataProvider
{
    public static class FetchStrategyExtensions
    {
        public static string ToIncludeString<T>(this Expression<Func<T, object>> selector)
        {
            var members = new List<PropertyInfo>();
            ExpressionHelper.CollectRelationalMembers(selector, members);

            var sb = new StringBuilder();
            var separator = "";
            foreach (var member in members)
            {
                sb.Append(separator);
                sb.Append(member.Name);
                separator = ".";
            }

            return sb.ToString();
        }
    }
}
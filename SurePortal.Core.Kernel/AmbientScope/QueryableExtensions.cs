using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Including<T>(this IQueryable<T> queryable, IncludingQuery<T> includingQuery)
            where T : class
        {
            return includingQuery == null
                ? queryable
                : includingQuery.Columns.Aggregate(queryable, (x, y) => x.Include(y));
        }

        public static IQueryable<T> Ordering<T>(this IQueryable<T> queryable, OrderingQuery<T> orderingQuery)
        {
            if (orderingQuery == null)
                return queryable;

            var first = true;

            // Loop through the order column to build the expression tree
            foreach (var column in orderingQuery.Columns)
            {
                // Get the property from the T, based on the key
                var lambda = column.Key;

                // Based on the order direction, get the right method
                string method;
                if (first)
                {
                    method = column.Value == OrderDirection.Ascending ? "OrderBy" : "OrderByDescending";
                    first = false;
                }
                else
                {
                    method = column.Value == OrderDirection.Ascending ? "ThenBy" : "ThenByDescending";
                }

                // itemType is the type of the TModel
                // exp.Body.Type is the type of the property.
                Type[] types = { typeof(T), lambda.ReturnType };

                // Build the call expression
                var methodCallExpression = Expression.Call(typeof(Queryable), method, types,
                    queryable.Expression, lambda);

                // Now you can run the expression against the collection
                queryable = queryable.Provider.CreateQuery<T>(methodCallExpression);
            }

            return queryable;
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, PagingQuery pagingQuery)
        {
            return pagingQuery == null ? queryable : queryable.Skip(pagingQuery.Skip).Take(pagingQuery.Take);
        }
    }
}
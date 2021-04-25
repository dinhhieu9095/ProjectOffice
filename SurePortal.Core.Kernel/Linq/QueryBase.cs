using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SurePortal.Core.Kernel.Linq
{
    public class QueryBase<T> : IQueryable<T>, IEnumerable<T>, IEnumerable, IQueryable
    {
        private readonly QueryProviderBase provider;

        public QueryBase(QueryProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            this.provider = provider;
            Expression = Expression.Constant(this);
        }

        public QueryBase(QueryProviderBase provider, Expression expression)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
                throw new ArgumentOutOfRangeException(nameof(expression));
            this.provider = provider;
            Expression = expression;
        }

        public Expression Expression { get; }

        public Type ElementType => typeof(T);

        public IQueryProvider Provider => provider;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)provider.Execute(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)provider.Execute(Expression)).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator(Expression e)
        {
            return ((IEnumerable<T>)provider.Execute(e)).GetEnumerator();
        }
    }
}
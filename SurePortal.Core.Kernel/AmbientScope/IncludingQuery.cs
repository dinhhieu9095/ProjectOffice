using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DaiPhatDat.Core.Kernel.AmbientScope
{
    public class IncludingQuery<T>
    {
        public IncludingQuery(IEnumerable<Expression<Func<T, object>>> columns)
        {
            Columns = columns ?? throw new ArgumentNullException(nameof(columns));
        }

        public IEnumerable<Expression<Func<T, object>>> Columns { get; }
    }
}
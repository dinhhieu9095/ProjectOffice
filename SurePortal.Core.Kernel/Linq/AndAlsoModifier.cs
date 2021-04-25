using System.Linq.Expressions;

namespace SurePortal.Core.Kernel.Linq
{
    public class AndAlsoModifier : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.AndAlso)
                return Expression.MakeBinary(ExpressionType.OrElse, Visit(b.Left), Visit(b.Right), b.IsLiftedToNull,
                    b.Method);
            return base.VisitBinary(b);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public class ExpressionStarter<T>
    {
        public bool IsStarted => _predicate != null;
        public Expression<Func<T, bool>> DefaultExpression { get; set; }
        public bool UseDefaultExpression => DefaultExpression != null;
        internal ExpressionStarter() : this(false) { }

        internal ExpressionStarter(bool defaultExpression)
        {
            if (defaultExpression)
                DefaultExpression = f => true;
            else
                DefaultExpression = f => false;
        }

        internal ExpressionStarter(Expression<Func<T, bool>> exp) : this(false)
        {
            _predicate = exp;
        }
        private Expression<Func<T, bool>> _predicate;
        private Expression<Func<T, bool>> Predicate => (IsStarted || !UseDefaultExpression) ? _predicate : DefaultExpression;
        public Expression<Func<T, bool>> And(Expression<Func<T, bool>> rightExpression)
        {
            return (IsStarted) ? _predicate = Predicate.And(rightExpression) : Start(rightExpression);
        }
        public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> rightExpression)
        {
            return (IsStarted) ? _predicate = Predicate.Or(rightExpression) : Start(rightExpression);
        }
        public Expression<Func<T, bool>> Start(Expression<Func<T, bool>> expression)
        {
            if (IsStarted)
                throw new Exception("Predicate cannot be started again.");

            return _predicate = expression;
        }
        public override string ToString()
        {
            return Predicate == null ? null : Predicate.ToString();
        }
        public Func<T, bool> Compile() { return Predicate.Compile(); }
        public static implicit operator ExpressionStarter<T>(Expression<Func<T, bool>> righteExpression)
        {
            return righteExpression == null ? null : new ExpressionStarter<T>(righteExpression);
        }
        public static implicit operator Func<T, bool>(ExpressionStarter<T> rightExpression)
        {
            return rightExpression == null ? null : (rightExpression.IsStarted || rightExpression.UseDefaultExpression) ? rightExpression.Predicate.Compile() : null;
        }
        public static implicit operator Expression<Func<T, bool>>(ExpressionStarter<T> rightExpression)
        {
            return rightExpression == null ? null : rightExpression.Predicate;
        }
    }
    internal class SubstExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
    {
        public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;
            if (subst.TryGetValue(node, out newValue))
            {
                return newValue;
            }
            return node;
        }
    }
    
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            ParameterExpression p = leftExpression.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[rightExpression.Parameters[0]] = p;

            Expression body = Expression.AndAlso(leftExpression.Body, visitor.Visit(rightExpression.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
        
        public static ExpressionStarter<T> New<T>(Expression<Func<T, bool>> expression = null)
        {
            return new ExpressionStarter<T>(expression);
        }
        
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            ParameterExpression p = leftExpression.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[rightExpression.Parameters[0]] = p;

            Expression body = Expression.OrElse(leftExpression.Body, visitor.Visit(rightExpression.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
    }
}

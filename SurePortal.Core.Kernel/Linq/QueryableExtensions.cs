using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DaiPhatDat.Core.Kernel.Linq
{
    /// <summary>
    /// Provides extension methods for Queryable source.
    /// <para></para>
    /// <para></para>
    /// <para>var fonts = FontFamily.Families.AsQueryable(); </para>
    /// <para></para>
    /// <para></para>
    /// <para>We would normally write Expressions as, </para>
    /// <para></para>
    /// <code lang="C#">var names = new string[] {"Tony", "Al",
    /// "Sean", "Elia"}.AsQueryable();
    /// names.OrderBy(n=&gt;n);</code>
    /// <para></para>
    /// <para></para>
    /// <para>This would sort the names based on alphabetical order. Like so, the
    /// Queryable extensions are a set of extension methods that define functions which
    /// will generate expressions based on the supplied values to the functions.</para>
    /// </summary>
    public static class QueryableExtensions
    {
        private static MethodInfo[] tMethod;
        private static MethodInfo[] emethods;

        public static IEnumerable OfQueryable(this IEnumerable items)
        {
            IEnumerator enumerator = items.GetEnumerator();
            if (!enumerator.MoveNext())
                return items;
            Type type = enumerator.Current.GetType();
            return (IEnumerable)items.AsQueryable().OfType(type);
        }

        public static IEnumerable OfQueryable(this IEnumerable items, Type sourceType)
        {
            return (IEnumerable)items.AsQueryable().OfType(sourceType);
        }

        /// <summary>
        /// Generates an AND binary expression for the given Binary expressions.
        /// <para></para>
        /// </summary>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        public static BinaryExpression AndPredicate(
          this Expression expr1,
          Expression expr2)
        {
            return Expression.And(expr1, expr2);
        }

        public static BinaryExpression AndAlsoPredicate(
          this Expression expr1,
          Expression expr2)
        {
            return Expression.AndAlso(expr1, expr2);
        }

        public static BinaryExpression OrElsePredicate(
          this Expression expr1,
          Expression expr2)
        {
            return Expression.OrElse(expr1, expr2);
        }

        public static int Count(this IQueryable source)
        {
            Type elementType = source.ElementType;
            return (int)source.Provider.Execute((Expression)Expression.Call(typeof(Queryable), nameof(Count), new Type[1]
            {
        elementType
            }, source.Expression));
        }

        public static object ElementAt(this IQueryable source, int index, Type sourceType)
        {
            return source.Provider.Execute((Expression)Expression.Call(typeof(Queryable), nameof(ElementAt), new Type[1]
            {
        sourceType
            }, source.Expression, (Expression)Expression.Constant((object)index)));
        }

        public static object ElementAt(this IQueryable source, int index)
        {
            Type elementType = source.ElementType;
            return source.ElementAt(index, elementType);
        }

        public static object ElementAtOrDefault(this IQueryable source, int index, Type sourceType)
        {
            return source.Provider.Execute((Expression)Expression.Call(typeof(Queryable), nameof(ElementAtOrDefault), new Type[1]
            {
        sourceType
            }, source.Expression, (Expression)Expression.Constant((object)index)));
        }

        public static object ElementAtOrDefault(this IQueryable source, int index)
        {
            Type elementType = source.ElementType;
            return source.ElementAtOrDefault(index, elementType);
        }

        public static IQueryable OfType(this IQueryable source, Type sourceType)
        {
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(OfType), new Type[1]
            {
        sourceType
            }, source.Expression));
        }

        /// <summary>
        /// Generates a OrderBy query for the Queryable source.
        /// <para></para>
        /// <code lang="C#">            DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///             var orders = db.Orders.Skip(0).Take(10).ToList();
        ///             var queryable = orders.AsQueryable();
        ///             var sortedOrders =
        /// queryable.OrderBy("ShipCountry");</code>
        /// <para></para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="sourceType"></param>
        public static IQueryable OrderBy(
          this IQueryable source,
          string propertyName,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression propertyNullCheck = QueryableExtensions.GetLambdaWithComplexPropertyNullCheck(source, propertyName, paramExpression, sourceType);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(OrderBy), new Type[2]
            {
        source.ElementType,
        propertyNullCheck.Body.Type
            }, source.Expression, (Expression)propertyNullCheck));
        }

        private static LambdaExpression GetLambdaWithComplexPropertyNullCheck(
          IQueryable source,
          string propertyName,
          ParameterExpression paramExpression,
          Type sourceType)
        {
            string[] strArray = propertyName.Split('.');
            LambdaExpression lambdaExpression;
            if (strArray.GetLength(0) > 1)
            {
                Expression expression1 = paramExpression.GetValueExpression(propertyName, sourceType);
                if (expression1.Type != typeof(object))
                    expression1 = (Expression)Expression.Convert(expression1, typeof(object));
                Expression expression2 = (Expression)null;
                string propertyName1 = string.Empty;
                int length = strArray.GetLength(0);
                for (int index = 0; index < length; ++index)
                {
                    if (index == 0)
                    {
                        expression2 = (Expression)Expression.Equal(paramExpression.GetValueExpression(strArray[index], sourceType), (Expression)Expression.Constant((object)null));
                        propertyName1 = strArray[index];
                    }
                    else if (index < length - 1)
                    {
                        propertyName1 = propertyName1 + "." + strArray[index];
                        expression2 = (Expression)Expression.OrElse(expression2, (Expression)Expression.Equal(paramExpression.GetValueExpression(propertyName1, sourceType), (Expression)Expression.Constant((object)null)));
                    }
                }
                lambdaExpression = Expression.Lambda((Expression)Expression.Condition(expression2, (Expression)Expression.Constant((object)null), expression1), paramExpression);
            }
            else
                lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            return lambdaExpression;
        }

        public static IQueryable OrderBy(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.OrderBy(propertyName, elementType);
        }

        public static IQueryable OrderBy(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression paramExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            InvocationExpression invocationExpression = Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)paramExpression);
            return source.OrderBy(paramExpression, (Expression)invocationExpression);
        }

        public static IQueryable OrderBy(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression1 = Expression.Constant((object)propertyName);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression1, (Expression)parameterExpression), parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(OrderBy))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression2 = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression2
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable OrderBy(
          this IQueryable source,
          ParameterExpression paramExpression,
          Expression mExp)
        {
            LambdaExpression lambdaExpression = Expression.Lambda(mExp, paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(OrderBy), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        /// <summary>
        /// Generates an OrderBy query for the IComparer defined.
        /// <para></para>
        /// <para> </para>
        /// <code lang="C#">   public class OrdersComparer :
        /// IComparer&lt;Order&gt;
        ///     {
        ///         public int Compare(Order x, Order y)
        ///         {
        ///             return string.Compare(x.ShipCountry, y.ShipCountry);
        ///         }
        ///     }</code>
        /// <para></para>
        /// <para><code lang="C#">var sortedOrders =
        /// db.Orders.Skip(0).Take(5).ToList().OrderBy(o =&gt; o, new
        /// OrdersComparer());</code></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="sourceType"></param>
        public static IQueryable OrderBy<T>(
          this IQueryable source,
          IComparer<T> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(OrderBy))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<T>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable OrderBy<T>(this IQueryable source, IComparer<T> comparer)
        {
            Type elementType = source.ElementType;
            return source.OrderBy<T>(comparer, elementType);
        }

        public static IQueryable OrderBy(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(OrderBy))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        /// <summary>
        /// Generates an OrderByDescending query for the IComparer defined.
        /// <para></para>
        /// <para> </para>
        /// <code lang="C#">   public class OrdersComparer :
        /// IComparer&lt;Order&gt;
        ///     {
        ///         public int Compare(Order x, Order y)
        ///         {
        ///             return string.Compare(x.ShipCountry, y.ShipCountry);
        ///         }
        ///     }</code>
        /// <para></para>
        /// <para><code lang="C#">var sortedOrders =
        /// db.Orders.Skip(0).Take(5).ToList().OrderByDescending(o =&gt; o, new
        /// OrdersComparer());</code></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="sourceType"></param>
        public static IQueryable OrderByDescending<T>(
          this IQueryable source,
          IComparer<T> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(OrderByDescending))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<T>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable OrderByDescending(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(OrderByDescending))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable OrderByDescending<T>(
          this IQueryable source,
          IComparer<T> comparer)
        {
            Type elementType = source.ElementType;
            return source.OrderByDescending<T>(comparer, elementType);
        }

        /// <summary>
        /// Generates a OrderByDescending query for the Queryable source.
        /// <para></para>
        /// <code lang="C#">            DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///             var orders = db.Orders.Skip(0).Take(10).ToList();
        ///             var queryable = orders.AsQueryable();
        ///             var sortedOrders =
        /// queryable.OrderByDescending("ShipCountry");</code>
        /// <para></para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="sourceType"></param>
        public static IQueryable OrderByDescending(
          this IQueryable source,
          string propertyName,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression propertyNullCheck = QueryableExtensions.GetLambdaWithComplexPropertyNullCheck(source, propertyName, paramExpression, sourceType);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(OrderByDescending), new Type[2]
            {
        source.ElementType,
        propertyNullCheck.Body.Type
            }, source.Expression, (Expression)propertyNullCheck));
        }

        public static Expression GetExpression(
          this ParameterExpression paramExpression,
          string propertyName)
        {
            return paramExpression.GetValueExpression(propertyName, paramExpression.Type);
        }

        /// <summary>Generate expression from simple and complex property</summary>
        /// <param name="propertyName"></param>
        /// <param name="sourceType"></param>
        /// <param name="paramExpression"></param>
        /// <returns></returns>
        public static Expression GetValueExpression(
          this ParameterExpression paramExpression,
          string propertyName,
          Type sourceType)
        {
            Expression expression1 = (Expression)null;
            string str = propertyName;
            char[] chArray = new char[1] { '.' };
            foreach (string propertyOrFieldName in str.Split(chArray))
            {
                if (expression1 != null)
                {
                    expression1 = (Expression)Expression.PropertyOrField(expression1, propertyOrFieldName);
                }
                else
                {
                    Expression expression2 = (Expression)paramExpression;
                    if (paramExpression.Type != sourceType)
                        expression2 = (Expression)Expression.Convert((Expression)paramExpression, sourceType);
                    expression1 = (Expression)Expression.PropertyOrField(expression2, propertyOrFieldName);
                }
            }
            return expression1;
        }

        public static IQueryable OrderByDescending(
          this IQueryable source,
          string propertyName)
        {
            Type elementType = source.ElementType;
            return source.OrderByDescending(propertyName, elementType);
        }

        public static IQueryable OrderByDescending(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression1 = Expression.Constant((object)propertyName);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression1, (Expression)parameterExpression), parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(OrderByDescending))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression2 = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression2
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable OrderByDescending(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression paramExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            InvocationExpression invocationExpression = Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)paramExpression);
            return source.OrderByDescending(paramExpression, (Expression)invocationExpression);
        }

        public static IQueryable OrderByDescending(
          this IQueryable source,
          ParameterExpression paramExpression,
          Expression mExp)
        {
            LambdaExpression lambdaExpression = Expression.Lambda(mExp, paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(OrderByDescending), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        /// <summary>
        /// Generates an OR binary expression for the given Binary expressions.
        /// <para></para>
        /// </summary>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        public static BinaryExpression OrPredicate(
          this Expression expr1,
          Expression expr2)
        {
            return Expression.Or(expr1, expr2);
        }

        /// <summary>
        /// Creates a ParameterExpression that is required when building a series of
        /// predicates for the WHERE filter.
        /// <para></para>
        /// <code lang="C#">        DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///         var orders = db.Orders.Skip(0).Take(100).ToList();
        ///         var queryable = orders.AsQueryable();
        ///         var parameter =
        /// queryable.Parameter();</code>
        /// <para></para>
        /// <para></para>Use this same parameter passed to generate different predicates and
        /// finally to generate the Lambda.
        /// </summary>
        /// <remarks>
        /// If we specify a parameter for every predicate, then the Lambda expression scope
        /// will be out of the WHERE query that gets generated.
        /// </remarks>
        /// <param name="source"></param>
        public static ParameterExpression Parameter(this IQueryable source)
        {
            Type elementType = source.ElementType;
            return Expression.Parameter(elementType, elementType.Name);
        }

        public static ParameterExpression Parameter(this Type sourceType)
        {
            return Expression.Parameter(sourceType, sourceType.Name);
        }

        public static Expression Equal(
          this ParameterExpression paramExpression,
          string propertyName,
          object value)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, paramExpression.Type);
            object obj = NullableHelperInternal.ChangeType(NullableHelperInternal.FixDbNUllasNull(value, valueExpression.Type), valueExpression.Type);
            return (Expression)Expression.Equal(valueExpression, (Expression)Expression.Constant(obj, valueExpression.Type));
        }

        public static BinaryExpression Equal(
          this ParameterExpression paramExpression,
          string propertyName,
          string propertyName2)
        {
            return Expression.Equal(paramExpression.GetExpression(propertyName), paramExpression.GetExpression(propertyName));
        }

        public static Expression Equal(
          this ParameterExpression paramExpression,
          string propertyName,
          object value,
          Type elementType,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Expression left = (Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
       });
            value = NullableHelperInternal.ChangeType(value, elementType);
            Expression expression = (Expression)Expression.Constant(value);
            if (expression.Type != elementType)
                expression = (Expression)Expression.Convert(expression, left.Type);
            return (Expression)Expression.Equal(left, expression);
        }

        public static Expression NotEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          object value,
          Type elementType,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Expression left = (Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
       });
            value = NullableHelperInternal.ChangeType(value, elementType);
            Expression expression = (Expression)Expression.Constant(value);
            if (expression.Type != elementType)
                expression = (Expression)Expression.Convert(expression, left.Type);
            return (Expression)Expression.NotEqual(left, expression);
        }

        public static BinaryExpression NotEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          object value)
        {
            Expression expression = paramExpression.GetExpression(propertyName);
            object obj = NullableHelperInternal.ChangeType(NullableHelperInternal.FixDbNUllasNull(value, expression.Type), expression.Type);
            return Expression.NotEqual(expression, (Expression)Expression.Constant(obj, expression.Type));
        }

        public static BinaryExpression NotEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          string propertyName2)
        {
            return Expression.NotEqual(paramExpression.GetExpression(propertyName), paramExpression.GetExpression(propertyName2));
        }

        public static BinaryExpression GreaterThanOrEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          object value)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, paramExpression.Type);
            object obj = NullableHelperInternal.ChangeType(NullableHelperInternal.FixDbNUllasNull(value, valueExpression.Type), valueExpression.Type);
            return Expression.GreaterThanOrEqual(valueExpression, (Expression)Expression.Constant(obj, valueExpression.Type));
        }

        public static BinaryExpression GreaterThanOrEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          string propertyName2)
        {
            return Expression.GreaterThanOrEqual(paramExpression.GetExpression(propertyName), paramExpression.GetExpression(propertyName2));
        }

        public static Expression GreaterThanOrEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          object value,
          Type elementType,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Expression left = (Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
       });
            value = NullableHelperInternal.ChangeType(value, elementType);
            Expression expression = (Expression)Expression.Constant(value);
            if (expression.Type != elementType)
                expression = (Expression)Expression.Convert(expression, left.Type);
            return (Expression)Expression.GreaterThanOrEqual(left, expression);
        }

        public static BinaryExpression GreaterThan(
          this ParameterExpression paramExpression,
          string propertyName,
          object value)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, paramExpression.Type);
            object obj = NullableHelperInternal.ChangeType(NullableHelperInternal.FixDbNUllasNull(value, valueExpression.Type), valueExpression.Type);
            return Expression.GreaterThan(valueExpression, (Expression)Expression.Constant(obj, valueExpression.Type));
        }

        public static BinaryExpression GreaterThan(
          this ParameterExpression paramExpression,
          string propertyName,
          string propertyName2)
        {
            return Expression.GreaterThan(paramExpression.GetExpression(propertyName), paramExpression.GetExpression(propertyName2));
        }

        public static Expression GreaterThan(
          this ParameterExpression paramExpression,
          string propertyName,
          object value,
          Type elementType,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Expression left = (Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
       });
            value = NullableHelperInternal.ChangeType(value, elementType);
            Expression expression = (Expression)Expression.Constant(value);
            if (expression.Type != elementType)
                expression = (Expression)Expression.Convert(expression, left.Type);
            return (Expression)Expression.GreaterThan(left, expression);
        }

        public static BinaryExpression LessThan(
          this ParameterExpression paramExpression,
          string propertyName,
          object value)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, paramExpression.Type);
            object obj = NullableHelperInternal.ChangeType(NullableHelperInternal.FixDbNUllasNull(value, valueExpression.Type), valueExpression.Type);
            return Expression.LessThan(valueExpression, (Expression)Expression.Constant(obj, valueExpression.Type));
        }

        public static BinaryExpression LessThan(
          this ParameterExpression paramExpression,
          string propertyName,
          string propertyName2)
        {
            return Expression.LessThan(paramExpression.GetExpression(propertyName), paramExpression.GetExpression(propertyName2));
        }

        public static Expression LessThan(
          this ParameterExpression paramExpression,
          string propertyName,
          object value,
          Type elementType,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Expression left = (Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
       });
            value = NullableHelperInternal.ChangeType(value, elementType);
            Expression expression = (Expression)Expression.Constant(value);
            if (expression.Type != elementType)
                expression = (Expression)Expression.Convert(expression, left.Type);
            return (Expression)Expression.LessThan(left, expression);
        }

        public static BinaryExpression LessThanOrEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          object value)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, paramExpression.Type);
            object obj = NullableHelperInternal.ChangeType(NullableHelperInternal.FixDbNUllasNull(value, valueExpression.Type), valueExpression.Type);
            return Expression.LessThanOrEqual(valueExpression, (Expression)Expression.Constant(obj, valueExpression.Type));
        }

        public static BinaryExpression LessThanOrEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          string propertyName2)
        {
            return Expression.LessThanOrEqual(paramExpression.GetValueExpression(propertyName, paramExpression.Type), paramExpression.GetExpression(propertyName2));
        }

        public static Expression LessThanOrEqual(
          this ParameterExpression paramExpression,
          string propertyName,
          object value,
          Type elementType,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Expression left = (Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
       });
            value = NullableHelperInternal.ChangeType(value, elementType);
            Expression expression = (Expression)Expression.Constant(value);
            if (expression.Type != elementType)
                expression = (Expression)Expression.Convert(expression, left.Type);
            return (Expression)Expression.LessThanOrEqual(left, expression);
        }

        /// <summary>
        /// Predicate is a Binary expression that needs to be built for a single or a series
        /// of values that needs to be passed on to the WHERE expression.
        /// <para></para>
        /// <para></para>
        /// <code lang="C#">var binaryExp = queryable.Predicate(parameter,
        /// "EmployeeID", "4", true);</code>
        /// </summary>
        /// <remarks>
        /// First create a ParameterExpression using the Parameter extension function, then
        /// use the same ParameterExpression to generate the predicates.
        /// </remarks>
        /// <param name="source"></param>
        /// <param name="paramExpression"></param>
        /// <param name="propertyName"></param>
        /// <param name="constValue"></param>
        /// <param name="filterType"></param>
        /// <param name="filteBehaviour"></param>
        /// <param name="isCaseSensitive"></param>
        /// <param name="sourceType"></param>
        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          FilterType filterType,
          FilterBehavior filteBehaviour,
          bool isCaseSensitive,
          Type sourceType)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, sourceType);
            Type type1 = valueExpression.Type;
            Type type2 = type1;
            bool flag = false;
            string[] strArray = propertyName.Split('.');
            int num = ((IEnumerable<string>)strArray).Count<string>();
            if (NullableHelperInternal.IsNullableType(type1))
                type2 = NullableHelperInternal.GetUnderlyingType(type1);
            object obj = NullableHelperInternal.FixDbNUllasNull(constValue, type1);
            if (obj != null)
            {
                if (type1.Name == "Boolean")
                {
                    if ("true".Contains(obj.ToString().ToLower()))
                        obj = (object)"1";
                    else if ("false".Contains(obj.ToString().ToLower()))
                        obj = (object)"0";
                }
                if (filterType != FilterType.StartsWith && filterType != FilterType.EndsWith && filterType != FilterType.Contains)
                {
                    try
                    {
                        if (obj is string && type1.Name != "Boolean")
                        {
                            if (ValueConvert.TryParse(obj.ToString(), type2))
                                obj = ValueConvert.ChangeType(obj, type2, (IFormatProvider)CultureInfo.CurrentCulture);
                            else
                                flag = true;
                        }
                        else
                            obj = ValueConvert.ChangeType(obj, type2, (IFormatProvider)CultureInfo.CurrentCulture);
                    }
                    catch
                    {
                        flag = true;
                    }
                }
            }
            Expression expr2_1 = (Expression)null;
            Type nullableType = NullableHelperInternal.GetNullableType(type1);
            if (!flag && (filterType == FilterType.Equals || filterType == FilterType.NotEquals || (filterType == FilterType.LessThan || filterType == FilterType.LessThanOrEqual) || filterType == FilterType.GreaterThan || filterType == FilterType.GreaterThanOrEqual))
            {
                switch (filterType)
                {
                    case FilterType.LessThan:
                        expr2_1 = (Expression)Expression.LessThan(valueExpression, (Expression)Expression.Constant(obj, type1));
                        break;

                    case FilterType.LessThanOrEqual:
                        expr2_1 = (Expression)Expression.LessThanOrEqual(valueExpression, (Expression)Expression.Constant(obj, type1));
                        break;

                    case FilterType.Equals:
                        expr2_1 = !(type2 != typeof(string)) ? (!isCaseSensitive ? (Expression)Expression.Equal((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(valueExpression, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj == null ? obj : (object)obj.ToString().ToLower(), typeof(string))) : (Expression)Expression.Equal(valueExpression, (Expression)Expression.Constant(obj, type1))) : (obj == null ? (Expression)Expression.Equal((Expression)Expression.Convert(valueExpression, nullableType), (Expression)Expression.Constant(obj, nullableType)) : (Expression)Expression.Equal(valueExpression, (Expression)Expression.Constant(obj, type1)));
                        break;

                    case FilterType.NotEquals:
                        expr2_1 = !(type2 != typeof(string)) ? (!isCaseSensitive ? (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(valueExpression, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj == null ? obj : (object)obj.ToString().ToLower(), type1)) : (Expression)Expression.NotEqual(valueExpression, (Expression)Expression.Constant(obj, type1))) : (obj == null ? (Expression)Expression.NotEqual((Expression)Expression.Convert(valueExpression, nullableType), (Expression)Expression.Constant(obj, nullableType)) : (Expression)Expression.NotEqual(valueExpression, (Expression)Expression.Constant(obj, type1)));
                        break;

                    case FilterType.GreaterThanOrEqual:
                        expr2_1 = (Expression)Expression.GreaterThanOrEqual(valueExpression, (Expression)Expression.Constant(obj, type1));
                        break;

                    case FilterType.GreaterThan:
                        expr2_1 = (Expression)Expression.GreaterThan(valueExpression, (Expression)Expression.Constant(obj, type1));
                        break;
                }
            }
            else
            {
                MethodInfo method1 = ((IEnumerable<MethodInfo>)valueExpression.Type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(d => d.Name == "ToString")).FirstOrDefault<MethodInfo>();
                Expression expression1;
                Expression left;
                if (valueExpression.Type == typeof(string))
                {
                    expression1 = (Expression)Expression.Coalesce(valueExpression, (Expression)Expression.Constant((object)string.Empty));
                    left = (Expression)Expression.Call(expression1, method1);
                }
                else
                {
                    left = (Expression)Expression.Call(valueExpression, method1);
                    expression1 = (Expression)Expression.Coalesce(left, (Expression)Expression.Constant((object)string.Empty));
                }
                MethodInfo method2 = ((IEnumerable<MethodInfo>)typeof(string).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m => m.Name == filterType.ToString())).FirstOrDefault<MethodInfo>();
                if (filterType == FilterType.NotEquals)
                {
                    Expression expression2 = (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(left, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj == null ? obj : (object)obj.ToString().ToLower(), typeof(string)));
                }
                Expression expression3;
                if (isCaseSensitive)
                    expression3 = (Expression)Expression.Call(expression1, method2, (Expression)Expression.Constant(obj, typeof(string)));
                else
                    expression3 = (Expression)Expression.Call((Expression)QueryableExtensions.GetToLowerMethodCallExpression(expression1), method2, (Expression)Expression.Constant(obj == null ? obj : (object)obj.ToString().ToLower(), typeof(string)));
                expr2_1 = expression3;
            }
            if (num > 1)
            {
                Expression expression1 = (Expression)null;
                Expression expression2 = (Expression)null;
                Expression expression3 = (Expression)paramExpression;
                ConstantExpression constantExpression1 = Expression.Constant(obj);
                ConstantExpression constantExpression2 = Expression.Constant((object)null);
                UnaryExpression unaryExpression = Expression.Convert((Expression)constantExpression1, typeof(object));
                BinaryExpression binaryExpression1 = Expression.Equal((Expression)unaryExpression, (Expression)constantExpression2);
                BinaryExpression binaryExpression2 = Expression.NotEqual((Expression)unaryExpression, (Expression)constantExpression2);
                for (int index = 0; index < num - 1; ++index)
                {
                    expression3 = (Expression)Expression.PropertyOrField(expression3, strArray[index]);
                    BinaryExpression binaryExpression3 = Expression.Equal(expression3, (Expression)constantExpression2);
                    expression1 = expression1 != null ? (Expression)Expression.OrElse(expression1, (Expression)binaryExpression3) : (Expression)binaryExpression3;
                    BinaryExpression binaryExpression4 = Expression.NotEqual(expression3, (Expression)constantExpression2);
                    expression2 = expression2 != null ? (Expression)Expression.AndAlso(expression2, (Expression)binaryExpression4) : (Expression)binaryExpression4;
                }
                if (expression1 != null)
                {
                    if (filterType == FilterType.Equals)
                        expression1 = (Expression)expression1.AndAlsoPredicate((Expression)binaryExpression1);
                    else if (filterType == FilterType.NotEquals)
                        expression1 = (Expression)expression1.AndAlsoPredicate((Expression)binaryExpression2);
                    Expression expr2_2 = (Expression)expression2.AndAlsoPredicate(expr2_1);
                    expr2_1 = (Expression)expression1.OrElsePredicate(expr2_2);
                }
            }
            return expr2_1;
        }

        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          FilterType filterType,
          bool isCaseSensitive,
          Type sourceType)
        {
            return source.Predicate(paramExpression, propertyName, constValue, filterType, FilterBehavior.StronglyTyped, isCaseSensitive, sourceType);
        }

        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          FilterType filterType,
          FilterBehavior filterBehaviour,
          bool isCaseSensitive,
          Type sourceType,
          Delegate expressionFunc,
          Delegate typeFunc)
        {
            IEnumerator enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return (Expression)null;
            Type memberType;
            if (typeFunc != null)
            {
                memberType = (Type)typeFunc.DynamicInvoke((object)propertyName, enumerator.Current);
                if (memberType == (Type)null)
                {
                    while (enumerator.MoveNext())
                    {
                        memberType = (Type)typeFunc.DynamicInvoke((object)propertyName, enumerator.Current);
                        if (memberType != (Type)null)
                            break;
                    }
                }
            }
            else
                memberType = expressionFunc.DynamicInvoke((object)propertyName, enumerator.Current).GetType();
            return source.Predicate(paramExpression, propertyName, constValue, memberType, filterType, filterBehaviour, isCaseSensitive, sourceType, expressionFunc, typeFunc);
        }

        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          FilterType filterType,
          bool isCaseSensitive,
          Type sourceType,
          Delegate expressionFunc,
          Delegate typeFunc)
        {
            return source.Predicate(paramExpression, propertyName, constValue, filterType, FilterBehavior.StronglyTyped, isCaseSensitive, sourceType, expressionFunc, typeFunc);
        }

        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          Type memberType,
          FilterType filterType,
          FilterBehavior filterBehaviour,
          bool isCaseSensitive,
          Type sourceType,
          Delegate expressionFunc,
          Delegate typeFunc)
        {
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
        {
            if (m.Name == "GetDelegateInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
                return m.IsGenericMethod;
            return false;
        }));
            Type type = memberType;
            if (NullableHelperInternal.IsNullableType(memberType))
                type = NullableHelperInternal.GetUnderlyingType(memberType);
            else if (constValue is DBNull || constValue == null)
            {
                if (memberType.GetTypeInfo().IsValueType)
                    memberType = typeof(Nullable<>).MakeGenericType(memberType);
                constValue = (object)null;
            }
            Expression expression1 = (Expression)methodInfo.MakeGenericMethod(memberType).Invoke((object)null, new object[3]
            {
        (object) paramExpression,
        (object) propertyName,
        (object) expressionFunc
            });
            object obj = NullableHelperInternal.FixDbNUllasNull(constValue, memberType);
            if (obj != null && filterType != FilterType.StartsWith && filterType != FilterType.EndsWith && filterType != FilterType.Contains)
                obj = ValueConvert.ChangeType(obj, type, (IFormatProvider)CultureInfo.CurrentCulture);
            if (filterType == FilterType.Equals || filterType == FilterType.NotEquals || (filterType == FilterType.LessThan || filterType == FilterType.LessThanOrEqual) || filterType == FilterType.GreaterThan || filterType == FilterType.GreaterThanOrEqual)
            {
                Expression expression2 = (Expression)null;
                switch (filterType)
                {
                    case FilterType.LessThan:
                        expression2 = (Expression)Expression.LessThan(expression1, (Expression)Expression.Constant(obj, memberType));
                        break;

                    case FilterType.LessThanOrEqual:
                        expression2 = (Expression)Expression.LessThanOrEqual(expression1, (Expression)Expression.Constant(obj, memberType));
                        break;

                    case FilterType.Equals:
                        expression2 = !(type != typeof(string)) && filterBehaviour != FilterBehavior.StringTyped ? (!isCaseSensitive ? (Expression)Expression.Equal((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(expression1, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj == null ? (object)string.Empty : (object)obj.ToString().ToLower(), expression1.Type)) : (Expression)Expression.Equal(expression1, (Expression)Expression.Constant(obj, memberType))) : (Expression)Expression.Equal(expression1, (Expression)Expression.Constant(obj, memberType));
                        break;

                    case FilterType.NotEquals:
                        expression2 = !(type != typeof(string)) ? (!isCaseSensitive ? (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(expression1, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj == null ? (object)string.Empty : (object)obj.ToString().ToLower(), memberType)) : (Expression)Expression.NotEqual(expression1, (Expression)Expression.Constant(obj, memberType))) : (Expression)Expression.NotEqual(expression1, (Expression)Expression.Constant(obj, memberType));
                        break;

                    case FilterType.GreaterThanOrEqual:
                        expression2 = (Expression)Expression.GreaterThanOrEqual(expression1, (Expression)Expression.Constant(obj, memberType));
                        break;

                    case FilterType.GreaterThan:
                        expression2 = (Expression)Expression.GreaterThan(expression1, (Expression)Expression.Constant(obj, memberType));
                        break;
                }
                return expression2;
            }
            MethodInfo method1 = ((IEnumerable<MethodInfo>)expression1.Type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(d => d.Name == "ToString")).FirstOrDefault<MethodInfo>();
            Expression expression3;
            Expression left;
            if (expression1.Type == typeof(string))
            {
                expression3 = (Expression)Expression.Coalesce(expression1, (Expression)Expression.Constant((object)string.Empty));
                left = (Expression)Expression.Call(expression3, method1);
            }
            else
            {
                left = (Expression)Expression.Call(expression1, method1);
                expression3 = (Expression)Expression.Coalesce(left, (Expression)Expression.Constant((object)string.Empty));
            }
            MethodInfo method2 = ((IEnumerable<MethodInfo>)typeof(string).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m => m.Name == filterType.ToString())).FirstOrDefault<MethodInfo>();
            if (filterType == FilterType.NotEquals)
                return (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(left, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj == null ? obj : (object)obj.ToString().ToLower(), typeof(string)));
            if (isCaseSensitive)
                return (Expression)Expression.Call(expression3, method2, (Expression)Expression.Constant(obj, typeof(string)));
            return (Expression)Expression.Call((Expression)QueryableExtensions.GetToLowerMethodCallExpression(expression3), method2, (Expression)Expression.Constant(obj == null ? obj : (object)obj.ToString().ToLower(), typeof(string)));
        }

        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          Type memberType,
          FilterType filterType,
          bool isCaseSensitive,
          Type sourceType,
          Delegate expressionFunc,
          Delegate typeFunc)
        {
            return source.Predicate(paramExpression, propertyName, constValue, memberType, filterType, FilterBehavior.StronglyTyped, isCaseSensitive, sourceType, expressionFunc, typeFunc);
        }

        public static Expression Predicate(
          this IQueryable source,
          ParameterExpression paramExpression,
          string propertyName,
          object constValue,
          FilterType filterType,
          FilterBehavior filteBehaviour,
          bool isCaseSensitive,
          Type sourceType,
          string format)
        {
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, sourceType);
            Type type1 = valueExpression.Type;
            Type type2 = type1;
            if (NullableHelperInternal.IsNullableType(type1))
                type2 = NullableHelperInternal.GetUnderlyingType(type1);
            object obj1 = NullableHelperInternal.FixDbNUllasNull(constValue, type1);
            if (obj1 != null && type1.Name == "Boolean")
            {
                if ("true".Contains(obj1.ToString().ToLower()))
                    obj1 = (object)"1";
                else if ("false".Contains(obj1.ToString().ToLower()))
                    obj1 = (object)"0";
            }
            if (filterType == FilterType.Equals || filterType == FilterType.NotEquals || (filterType == FilterType.LessThan || filterType == FilterType.LessThanOrEqual) || filterType == FilterType.GreaterThan || filterType == FilterType.GreaterThanOrEqual)
            {
                Expression expression = (Expression)null;
                switch (filterType)
                {
                    case FilterType.LessThan:
                        if (!string.IsNullOrEmpty(format))
                        {
                            object obj2 = ValueConvert.ChangeType(obj1, type2, (IFormatProvider)CultureInfo.CurrentCulture, format, true);
                            expression = (Expression)Expression.LessThan(valueExpression, (Expression)Expression.Constant(obj2, type1));
                            break;
                        }
                        expression = (Expression)Expression.LessThan(valueExpression, (Expression)Expression.Constant(obj1, type1));
                        break;

                    case FilterType.LessThanOrEqual:
                        if (!string.IsNullOrEmpty(format))
                        {
                            Expression right = (Expression)Expression.Equal((Expression)QueryableExtensions.GetFormatMethodCallExpression(valueExpression, format), (Expression)Expression.Constant(obj1, typeof(string)));
                            object obj2 = ValueConvert.ChangeType(obj1, type2, (IFormatProvider)CultureInfo.CurrentCulture, format, true);
                            expression = (Expression)Expression.Or((Expression)Expression.LessThan(valueExpression, (Expression)Expression.Constant(obj2, type1)), right);
                            break;
                        }
                        expression = (Expression)Expression.LessThanOrEqual(valueExpression, (Expression)Expression.Constant(obj1, type1));
                        break;

                    case FilterType.Equals:
                        expression = !(type2 != typeof(string)) ? (!isCaseSensitive ? (Expression)Expression.Equal((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(valueExpression, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj1 == null ? obj1 : (object)obj1.ToString().ToLower(), valueExpression.Type)) : (Expression)Expression.Equal(valueExpression, (Expression)Expression.Constant(obj1, type1))) : (string.IsNullOrEmpty(format) ? (Expression)Expression.Equal(valueExpression, (Expression)Expression.Constant(obj1, type1)) : (Expression)Expression.Equal((Expression)QueryableExtensions.GetFormatMethodCallExpression(valueExpression, format), (Expression)Expression.Constant(obj1, typeof(string))));
                        break;

                    case FilterType.NotEquals:
                        expression = !(type2 != typeof(string)) ? (!isCaseSensitive ? (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetToLowerMethodCallExpression((Expression)Expression.Coalesce(valueExpression, (Expression)Expression.Constant((object)string.Empty))), (Expression)Expression.Constant(obj1 == null ? obj1 : (object)obj1.ToString().ToLower(), type1)) : (Expression)Expression.NotEqual(valueExpression, (Expression)Expression.Constant(obj1, type1))) : (string.IsNullOrEmpty(format) ? (Expression)Expression.NotEqual(valueExpression, (Expression)Expression.Constant(obj1, type1)) : (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetFormatMethodCallExpression(valueExpression, format), (Expression)Expression.Constant(obj1, typeof(string))));
                        break;

                    case FilterType.GreaterThanOrEqual:
                        object obj3 = ValueConvert.ChangeType(obj1, type2, (IFormatProvider)CultureInfo.CurrentCulture, format, true);
                        expression = (Expression)Expression.GreaterThanOrEqual(valueExpression, (Expression)Expression.Constant(obj3, type1));
                        break;

                    case FilterType.GreaterThan:
                        if (!string.IsNullOrEmpty(format))
                        {
                            Expression right = (Expression)Expression.NotEqual((Expression)QueryableExtensions.GetFormatMethodCallExpression(valueExpression, format), (Expression)Expression.Constant(obj1, typeof(string)));
                            object obj2 = ValueConvert.ChangeType(obj1, type2, (IFormatProvider)CultureInfo.CurrentCulture, format, true);
                            expression = (Expression)Expression.And((Expression)Expression.GreaterThan(valueExpression, (Expression)Expression.Constant(obj2, type1)), right);
                            break;
                        }
                        expression = (Expression)Expression.GreaterThan(valueExpression, (Expression)Expression.Constant(obj1, type1));
                        break;
                }
                return expression;
            }
            if (string.IsNullOrEmpty(format))
            {
                MethodInfo method1 = ((IEnumerable<MethodInfo>)valueExpression.Type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(d => d.Name == "ToString")).FirstOrDefault<MethodInfo>();
                Expression expression = (Expression)Expression.Coalesce((Expression)Expression.Call(valueExpression, method1), (Expression)Expression.Constant((object)string.Empty));
                MethodInfo method2 = ((IEnumerable<MethodInfo>)typeof(string).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m => m.Name == filterType.ToString())).FirstOrDefault<MethodInfo>();
                if (isCaseSensitive)
                    return (Expression)Expression.Call(expression, method2, (Expression)Expression.Constant(obj1, typeof(string)));
                return (Expression)Expression.Call((Expression)QueryableExtensions.GetToLowerMethodCallExpression(expression), method2, (Expression)Expression.Constant(obj1 == null ? obj1 : (object)obj1.ToString().ToLower(), typeof(string)));
            }
            MethodCallExpression methodCallExpression = QueryableExtensions.GetFormatMethodCallExpression(valueExpression, format);
            MethodInfo method3 = ((IEnumerable<MethodInfo>)methodCallExpression.Type.GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(d => d.Name == "ToString")).FirstOrDefault<MethodInfo>();
            Expression expression1 = (Expression)Expression.Call((Expression)methodCallExpression, method3);
            Expression expression2 = (Expression)Expression.Coalesce((Expression)methodCallExpression, (Expression)Expression.Constant((object)string.Empty));
            MethodInfo method4 = ((IEnumerable<MethodInfo>)typeof(string).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m => m.Name == filterType.ToString())).FirstOrDefault<MethodInfo>();
            if (isCaseSensitive)
                return (Expression)Expression.Call(expression2, method4, (Expression)Expression.Constant(obj1, typeof(string)));
            return (Expression)Expression.Call((Expression)QueryableExtensions.GetToLowerMethodCallExpression(expression2), method4, (Expression)Expression.Constant(obj1 == null ? obj1 : (object)obj1.ToString().ToLower(), typeof(string)));
        }

        private static MethodCallExpression GetToLowerMethodCallExpression(
          Expression memExp)
        {
            MethodInfo method = ((IEnumerable<MethodInfo>)typeof(string).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m => m.Name == "ToLower"));
            return Expression.Call(memExp, method, new Expression[0]);
        }

        private static MethodCallExpression GetFormatMethodCallExpression(
          Expression memExp,
          string format)
        {
            if (memExp.Type.GetTypeInfo().IsGenericType && memExp.Type.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>))
                memExp = (Expression)Expression.Call(memExp, "GetValueOrDefault", Type.EmptyTypes);
            MethodInfo method = typeof(DateTime).GetMethod("ToString", new Type[2]
            {
        typeof (string),
        typeof (IFormatProvider)
            });
            if (memExp.Type == typeof(Decimal))
                method = typeof(Decimal).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            else if (memExp.Type == typeof(double))
                method = typeof(double).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            else if (memExp.Type == typeof(float))
                method = typeof(float).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            else if (memExp.Type == typeof(short))
                method = typeof(short).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            else if (memExp.Type == typeof(int))
                method = typeof(int).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            else if (memExp.Type == typeof(long))
                method = typeof(long).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            else if (memExp.Type == typeof(string))
                method = typeof(string).GetMethod("ToString", new Type[2]
                {
          typeof (string),
          typeof (IFormatProvider)
                });
            return Expression.Call(memExp, method, (Expression)Expression.Constant((object)format), (Expression)Expression.Constant((object)CultureInfo.CurrentCulture));
        }

        /// <summary>Generates a Select query for a single property value.</summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="sourceType"></param>
        public static IQueryable Select(
          this IQueryable source,
          string propertyName,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Select), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        public static IQueryable Select(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.Select(propertyName, elementType);
        }

        /// <summary>
        /// Generates a Select query based on the properties passed.
        /// <para></para>
        /// <code lang="C#">            DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///             var orders = db.Orders.Skip(0).Take(10).ToList();
        ///             var queryable = orders.AsQueryable();
        ///             var selector = queryable.Select(new string[]{
        /// "OrderID", "ShipCountry" });</code>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="properties"></param>
        public static IQueryable Select(this IQueryable source, params string[] properties)
        {
            return source.Select((IEnumerable<string>)Enumerable.ToList<string>((IEnumerable<string>)properties));
        }

        /// <summary>
        /// Generates a Select query based on the properties passed.
        /// <para></para>
        /// <code lang="C#">            DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///             var orders = db.Orders.Skip(0).Take(10).ToList();
        ///             var queryable = orders.AsQueryable();
        ///             var selector = queryable.Select(new List&lt;string&gt;() {
        /// "OrderID", "ShipCountry" });</code>
        /// <para></para>
        /// <para>It returns a dynamic class generated thru ReflectionEmit, Use reflection
        /// to identify the properties and values.</para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="properties"></param>
        /// <param name="sourceType"></param>
        public static IQueryable Select(
          this IQueryable source,
          IEnumerable<string> properties,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(QueryableExtensions.GenerateNew(properties, paramExpression), paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Select), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        public static IQueryable Select(
          this IQueryable source,
          IEnumerable<string> properties)
        {
            Type elementType = source.ElementType;
            return source.Select(properties, elementType);
        }

        /// <summary>Generates a SKIP expression in the IQueryable source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="constValue">The const value.</param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static IQueryable Skip(
          this IQueryable source,
          int constValue,
          Type sourceType)
        {
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Skip), new Type[1]
            {
        sourceType
            }, source.Expression, (Expression)Expression.Constant((object)constValue)));
        }

        public static IQueryable Skip(this IQueryable source, int constValue)
        {
            Type elementType = source.ElementType;
            return source.Skip(constValue, elementType);
        }

        private static MethodInfo[] tmethod
        {
            get
            {
                if (QueryableExtensions.tMethod != null)
                    return QueryableExtensions.tMethod;
                MethodInfo[] methods = typeof(Queryable).GetMethods();
                return QueryableExtensions.tMethod = ((IEnumerable<MethodInfo>)methods).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
              {
                  if (m.Name == "Sum")
                      return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
                  return false;
              })).ToArray<MethodInfo>();
            }
        }

        private static MethodInfo[] eMethods
        {
            get
            {
                if (QueryableExtensions.emethods != null)
                    return QueryableExtensions.emethods;
                MethodInfo[] methods = typeof(EnumerableExtensions).GetMethods();
                return QueryableExtensions.emethods = ((IEnumerable<MethodInfo>)methods).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
              {
                  if (m.Name == "Sum")
                      return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
                  return false;
              })).ToArray<MethodInfo>();
            }
        }

        public static object Sum(this IQueryable source, string propertyName, Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(sourceType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            Type type = lambdaExpression.Body.Type;
            MethodInfo methodInfo = (MethodInfo)null;
            if (NullableHelperInternal.IsNullableType(type))
            {
                string name = NullableHelperInternal.GetUnderlyingType(type).Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Int64"))
                    {
                        if (!(name == "Single"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = QueryableExtensions.tmethod[9];
                            }
                            else
                                methodInfo = QueryableExtensions.tmethod[7];
                        }
                        else
                            methodInfo = QueryableExtensions.tmethod[5];
                    }
                    else
                        methodInfo = QueryableExtensions.tmethod[3];
                }
                else
                    methodInfo = QueryableExtensions.tmethod[1];
            }
            else
            {
                string name = type.Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Int64"))
                    {
                        if (!(name == "Single"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = QueryableExtensions.tmethod[8];
                            }
                            else
                                methodInfo = QueryableExtensions.tmethod[6];
                        }
                        else
                            methodInfo = QueryableExtensions.tmethod[4];
                    }
                    else
                        methodInfo = QueryableExtensions.tmethod[2];
                }
                else
                    methodInfo = QueryableExtensions.tmethod[0];
            }
            if (methodInfo == (MethodInfo)null && (type.Name == "Int16" || NullableHelperInternal.GetUnderlyingType(type).Name == "Int16"))
                methodInfo = !NullableHelperInternal.IsNullableType(type) ? QueryableExtensions.eMethods[0] : QueryableExtensions.eMethods[2];
            if (!(methodInfo != (MethodInfo)null))
                return (object)null;
            return source.Provider.Execute((Expression)Expression.Call((Expression)null, methodInfo.MakeGenericMethod(sourceType), new Expression[2]
            {
        source.Expression,
        (Expression) Expression.Quote((Expression) lambdaExpression)
            }));
        }

        public static object Sum(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.Sum(propertyName, elementType);
        }

        public static object Sum(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc,
          Expression<Func<string, object, object>> typeFunc)
        {
            IEnumerator enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return (object)null;
            Type type;
            if (typeFunc != null)
                type = (Type)typeFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current);
            else
                type = expressionFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current).GetType();
            MethodInfo methodInfo = (MethodInfo)null;
            if (NullableHelperInternal.IsNullableType(type))
            {
                string name = NullableHelperInternal.GetUnderlyingType(type).Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Int64"))
                    {
                        if (!(name == "Single"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = QueryableExtensions.tmethod[9];
                            }
                            else
                                methodInfo = QueryableExtensions.tmethod[7];
                        }
                        else
                            methodInfo = QueryableExtensions.tmethod[5];
                    }
                    else
                        methodInfo = QueryableExtensions.tmethod[3];
                }
                else
                    methodInfo = QueryableExtensions.tmethod[1];
            }
            else
            {
                string name = type.Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Int64"))
                    {
                        if (!(name == "Single"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = QueryableExtensions.tmethod[8];
                            }
                            else
                                methodInfo = QueryableExtensions.tmethod[6];
                        }
                        else
                            methodInfo = QueryableExtensions.tmethod[4];
                    }
                    else
                        methodInfo = QueryableExtensions.tmethod[2];
                }
                else
                    methodInfo = QueryableExtensions.tmethod[0];
            }
            if (methodInfo == (MethodInfo)null && type.Name == "Int16")
            {
                MethodInfo[] array = ((IEnumerable<MethodInfo>)typeof(EnumerableExtensions).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
             {
                 if (m.Name == nameof(Sum))
                     return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
                 return false;
             })).ToArray<MethodInfo>();
                methodInfo = !NullableHelperInternal.IsNullableType(type) ? array[0] : array[1];
            }
            if (!(methodInfo != (MethodInfo)null))
                return (object)0;
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)parameterExpression);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(type).Invoke((object)null, new object[3]
            {
        (object) parameterExpression,
        (object) propertyName,
        (object) expressionFunc
       }), parameterExpression);
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(elementType), new Expression[2]
            {
        source.Expression,
        (Expression) Expression.Quote((Expression) lambdaExpression)
            });
            return source.Provider.Execute((Expression)methodCallExpression);
        }

        public static object Average(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.Average(propertyName, elementType);
        }

        public static object Average(this IQueryable source, string propertyName, Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(sourceType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            MethodInfo[] array1 = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(Average))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
             return false;
         })).ToArray<MethodInfo>();
            Type type = lambdaExpression.Body.Type;
            MethodInfo methodInfo = (MethodInfo)null;
            if (NullableHelperInternal.IsNullableType(type))
            {
                string name = NullableHelperInternal.GetUnderlyingType(type).Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Single"))
                    {
                        if (!(name == "Int64"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = array1[9];
                            }
                            else
                                methodInfo = array1[7];
                        }
                        else
                            methodInfo = array1[5];
                    }
                    else
                        methodInfo = array1[3];
                }
                else
                    methodInfo = array1[1];
            }
            else
            {
                string name = type.Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Single"))
                    {
                        if (!(name == "Int64"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = array1[8];
                            }
                            else
                                methodInfo = array1[6];
                        }
                        else
                            methodInfo = array1[4];
                    }
                    else
                        methodInfo = array1[2];
                }
                else
                    methodInfo = array1[0];
            }
            if (methodInfo == (MethodInfo)null && (type.Name == "Int16" || NullableHelperInternal.GetUnderlyingType(type).Name == "Int16"))
            {
                MethodInfo[] array2 = ((IEnumerable<MethodInfo>)typeof(EnumerableExtensions).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
             {
                 if (m.Name == nameof(Average))
                     return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
                 return false;
             })).ToArray<MethodInfo>();
                methodInfo = !NullableHelperInternal.IsNullableType(type) ? array2[0] : array2[2];
            }
            if (!(methodInfo != (MethodInfo)null))
                return (object)null;
            return source.Provider.Execute((Expression)Expression.Call((Expression)null, methodInfo.MakeGenericMethod(sourceType), new Expression[2]
            {
        source.Expression,
        (Expression) Expression.Quote((Expression) lambdaExpression)
            }));
        }

        public static object Average(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc,
          Expression<Func<string, object, object>> typeFunc)
        {
            MethodInfo[] array1 = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(Average))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
             return false;
         })).ToArray<MethodInfo>();
            IEnumerator enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return (object)null;
            Type type;
            if (typeFunc != null)
                type = (Type)typeFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current);
            else
                type = expressionFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current).GetType();
            MethodInfo methodInfo = (MethodInfo)null;
            if (NullableHelperInternal.IsNullableType(type))
            {
                string name = NullableHelperInternal.GetUnderlyingType(type).Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Single"))
                    {
                        if (!(name == "Int64"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = array1[9];
                            }
                            else
                                methodInfo = array1[7];
                        }
                        else
                            methodInfo = array1[5];
                    }
                    else
                        methodInfo = array1[3];
                }
                else
                    methodInfo = array1[1];
            }
            else
            {
                string name = type.Name;
                if (!(name == "Int32"))
                {
                    if (!(name == "Single"))
                    {
                        if (!(name == "Int64"))
                        {
                            if (!(name == "Double"))
                            {
                                if (name == "Decimal")
                                    methodInfo = array1[8];
                            }
                            else
                                methodInfo = array1[6];
                        }
                        else
                            methodInfo = array1[4];
                    }
                    else
                        methodInfo = array1[2];
                }
                else
                    methodInfo = array1[0];
            }
            if (methodInfo == (MethodInfo)null && type.Name == "Int16")
            {
                MethodInfo[] array2 = ((IEnumerable<MethodInfo>)typeof(EnumerableExtensions).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
             {
                 if (m.Name == nameof(Average))
                     return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
                 return false;
             })).ToArray<MethodInfo>();
                methodInfo = !NullableHelperInternal.IsNullableType(type) ? array2[0] : array2[1];
            }
            if (!(methodInfo != (MethodInfo)null))
                return (object)0;
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)parameterExpression);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(type).Invoke((object)null, new object[3]
            {
        (object) parameterExpression,
        (object) propertyName,
        (object) expressionFunc
       }), parameterExpression);
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(elementType), new Expression[2]
            {
        source.Expression,
        (Expression) Expression.Quote((Expression) lambdaExpression)
            });
            return source.Provider.Execute((Expression)methodCallExpression);
        }

        public static object Max(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.Max(propertyName, elementType);
        }

        public static object Max(this IQueryable source, string propertyName, Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(sourceType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
        {
            if (m.Name == nameof(Max))
                return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
            return false;
        })).MakeGenericMethod(sourceType, lambdaExpression.Body.Type), new Expression[2]
            {
        source.Expression,
        (Expression) lambdaExpression
        });
            return source.Provider.Execute((Expression)methodCallExpression);
        }

        public static object Max(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc,
          Expression<Func<string, object, object>> typeFunc)
        {
            IEnumerator enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return (object)null;
            Type type;
            if (typeFunc != null)
                type = (Type)typeFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current);
            else
                type = expressionFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current).GetType();
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)parameterExpression);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(type).Invoke((object)null, new object[3]
            {
        (object) parameterExpression,
        (object) propertyName,
        (object) expressionFunc
       }), parameterExpression);
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
        {
            if (m.Name == nameof(Max))
                return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
            return false;
        })).FirstOrDefault<MethodInfo>().MakeGenericMethod(elementType, type), new Expression[2]
            {
        source.Expression,
        (Expression) Expression.Quote((Expression) lambdaExpression)
        });
            return source.Provider.Execute((Expression)methodCallExpression);
        }

        public static object Min(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.Min(propertyName, elementType);
        }

        public static object Min(this IQueryable source, string propertyName, Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(sourceType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
        {
            if (m.Name == nameof(Min))
                return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
            return false;
        })).MakeGenericMethod(sourceType, lambdaExpression.Body.Type), new Expression[2]
            {
        source.Expression,
        (Expression) lambdaExpression
        });
            return source.Provider.Execute((Expression)methodCallExpression);
        }

        public static object Min(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc,
          Expression<Func<string, object, object>> typeFunc)
        {
            IEnumerator enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                return (object)null;
            Type type;
            if (typeFunc != null)
                type = (Type)typeFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current);
            else
                type = expressionFunc.Compile().DynamicInvoke((object)propertyName, enumerator.Current).GetType();
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)parameterExpression);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)((IEnumerable<MethodInfo>)((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).ToArray<MethodInfo>()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
       {
           if (m.Name == "GetInvokeExpressionAggregateFunc" && m.IsStatic && m.IsPrivate)
               return m.IsGenericMethod;
           return false;
       })).MakeGenericMethod(type).Invoke((object)null, new object[3]
            {
        (object) parameterExpression,
        (object) propertyName,
        (object) expressionFunc
       }), parameterExpression);
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
        {
            if (m.Name == nameof(Min))
                return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 2;
            return false;
        })).MakeGenericMethod(elementType, type), new Expression[2]
            {
        source.Expression,
        (Expression) Expression.Quote((Expression) lambdaExpression)
        });
            return source.Provider.Execute((Expression)methodCallExpression);
        }

        /// <summary>Generates a TAKE expression in the IQueryable source.</summary>
        /// <param name="source">The source.</param>
        /// <param name="constValue">The const value.</param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static IQueryable Take(
          this IQueryable source,
          int constValue,
          Type sourceType)
        {
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Take), new Type[1]
            {
        sourceType
            }, source.Expression, (Expression)Expression.Constant((object)constValue)));
        }

        public static IQueryable Take(this IQueryable source, int constValue)
        {
            Type elementType = source.ElementType;
            return source.Take(constValue, elementType);
        }

        /// <summary>
        /// Generates a ThenBy query for the Queryable source.
        /// <para></para>
        /// <code lang="C#">            DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///             var orders = db.Orders.Skip(0).Take(10).ToList();
        ///             var queryable = orders.AsQueryable();
        ///             var sortedOrders = queryable.OrderBy("ShipCountry");
        ///             sortedOrders = sortedOrders.ThenBy("ShipCity");</code>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="sourceType"></param>
        public static IQueryable ThenBy(
          this IQueryable source,
          string propertyName,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(ThenBy), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        public static IQueryable ThenBy(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.ThenBy(propertyName, elementType);
        }

        public static IQueryable ThenBy(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression1 = Expression.Constant((object)propertyName);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression1, (Expression)parameterExpression), parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(ThenBy))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression2 = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression2
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable ThenBy(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression paramExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            InvocationExpression invocationExpression = Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)paramExpression);
            return source.ThenBy(paramExpression, (Expression)invocationExpression);
        }

        public static IQueryable ThenBy(
          this IQueryable source,
          ParameterExpression paramExpression,
          Expression mExp)
        {
            LambdaExpression lambdaExpression = Expression.Lambda(mExp, paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(ThenBy), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        /// <summary>
        /// Generates an ThenBy query for the IComparer defined.
        /// <para></para>
        /// <para> </para>
        /// <code lang="C#">   public class OrdersComparer :
        /// IComparer&lt;Order&gt;
        ///     {
        ///         public int Compare(Order x, Order y)
        ///         {
        ///             return string.Compare(x.ShipCountry, y.ShipCountry);
        ///         }
        ///     }</code>
        /// <para></para>
        /// <para><code lang="C#">var sortedOrders =
        /// db.Orders.Skip(0).Take(5).ToList().ThenBy(o =&gt; o, new
        /// OrdersComparer());</code></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="sourceType"></param>
        public static IQueryable ThenBy<T>(
          this IQueryable source,
          IComparer<T> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(ThenBy))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<T>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable ThenBy(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(ThenBy))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable ThenBy<T>(this IQueryable source, IComparer<T> comparer)
        {
            Type elementType = source.ElementType;
            return source.ThenBy<T>(comparer, elementType);
        }

        /// <summary>
        /// Generates an ThenByDescending query for the IComparer defined.
        /// <para></para>
        /// <para> </para>
        /// <code lang="C#">   public class OrdersComparer :
        /// IComparer&lt;Order&gt;
        ///     {
        ///         public int Compare(Order x, Order y)
        ///         {
        ///             return string.Compare(x.ShipCountry, y.ShipCountry);
        ///         }
        ///     }</code>
        /// <para></para>
        /// <para><code lang="C#">var sortedOrders =
        /// db.Orders.Skip(0).Take(5).ToList().ThenByDescending(o =&gt; o, new
        /// OrdersComparer());</code></para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <param name="sourceType"></param>
        public static IQueryable ThenByDescending<T>(
          this IQueryable source,
          IComparer<T> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(ThenByDescending))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<T>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable ThenByDescending(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Type sourceType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)parameterExpression, parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(ThenByDescending))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable ThenByDescending<T>(
          this IQueryable source,
          IComparer<T> comparer)
        {
            Type elementType = source.ElementType;
            return source.ThenByDescending<T>(comparer, elementType);
        }

        public static IQueryable ThenByDescending(
          this IQueryable source,
          string propertyName,
          IComparer<object> comparer,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression parameterExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression1 = Expression.Constant((object)propertyName);
            LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression1, (Expression)parameterExpression), parameterExpression);
            MethodInfo methodInfo = ((IEnumerable<MethodInfo>)typeof(Queryable).GetMethods()).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == nameof(ThenByDescending))
                 return ((IEnumerable<ParameterInfo>)m.GetParameters()).Count<ParameterInfo>() == 3;
             return false;
         }));
            ConstantExpression constantExpression2 = Expression.Constant((object)comparer, typeof(IComparer<object>));
            MethodCallExpression methodCallExpression = Expression.Call((Expression)null, methodInfo.MakeGenericMethod(source.ElementType, lambdaExpression.Body.Type), new Expression[3]
            {
        source.Expression,
        (Expression) lambdaExpression,
        (Expression) constantExpression2
            });
            return source.Provider.CreateQuery((Expression)methodCallExpression);
        }

        public static IQueryable ThenByDescending(
          this IQueryable source,
          string propertyName,
          Expression<Func<string, object, object>> expressionFunc)
        {
            Type elementType = source.ElementType;
            ParameterExpression paramExpression = Expression.Parameter(elementType, elementType.Name);
            ConstantExpression constantExpression = Expression.Constant((object)propertyName);
            InvocationExpression invocationExpression = Expression.Invoke((Expression)expressionFunc, (Expression)constantExpression, (Expression)paramExpression);
            return source.ThenByDescending(paramExpression, (Expression)invocationExpression);
        }

        public static IQueryable ThenByDescending(
          this IQueryable source,
          ParameterExpression paramExpression,
          Expression mExp)
        {
            LambdaExpression lambdaExpression = Expression.Lambda(mExp, paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(ThenByDescending), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        /// <summary>
        /// Generates a ThenByDescending query for the Queryable source.
        /// <para></para>
        /// <code lang="C#">            DataClasses1DataContext db = new
        /// DataClasses1DataContext();
        ///             var orders = db.Orders.Skip(0).Take(10).ToList();
        ///             var queryable = orders.AsQueryable();
        ///             var sortedOrders = queryable.OrderBy("ShipCountry");
        ///             sortedOrders = sortedOrders.ThenByDescending("ShipCity");</code>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="sourceType"></param>
        public static IQueryable ThenByDescending(
          this IQueryable source,
          string propertyName,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(paramExpression.GetValueExpression(propertyName, sourceType), paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(ThenByDescending), new Type[2]
            {
        source.ElementType,
        lambdaExpression.Body.Type
            }, source.Expression, (Expression)lambdaExpression));
        }

        public static IQueryable ThenByDescending(this IQueryable source, string propertyName)
        {
            Type elementType = source.ElementType;
            return source.ThenByDescending(propertyName, elementType);
        }

        /// <summary>
        /// Generates the where expression.
        /// <para></para>
        /// <code lang="C#">            var nw = new Northwind(@"Data Source =
        /// Northwind.sdf");
        ///             IQueryable queryable = nw.Orders.AsQueryable();
        ///             var filters = queryable.Where("ShipCountry",
        /// "z", FilterType.Contains);
        ///             foreach (Orders item in filters)
        ///             {
        ///                 Console.WriteLine("{0}/{1}", item.OrderID,
        /// item.ShipCountry);
        ///             }</code>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value"></param>
        /// <param name="filterType"></param>
        /// <param name="isCaseSensitive"></param>
        /// <param name="sourceType"></param>
        public static IQueryable Where(
          this IQueryable source,
          string propertyName,
          object value,
          FilterType filterType,
          bool isCaseSensitive,
          Type sourceType)
        {
            ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
            Expression valueExpression = paramExpression.GetValueExpression(propertyName, sourceType);
            Type type = valueExpression.Type;
            if (NullableHelperInternal.IsNullableType(valueExpression.Type))
                type = NullableHelperInternal.GetUnderlyingType(valueExpression.Type);
            if (filterType == FilterType.Equals || filterType == FilterType.NotEquals || (filterType == FilterType.LessThan || filterType == FilterType.LessThanOrEqual) || filterType == FilterType.GreaterThan || filterType == FilterType.GreaterThanOrEqual)
            {
                BinaryExpression binaryExpression = (BinaryExpression)null;
                switch (filterType)
                {
                    case FilterType.LessThan:
                        binaryExpression = Expression.LessThan(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type));
                        break;

                    case FilterType.LessThanOrEqual:
                        binaryExpression = Expression.LessThanOrEqual(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type));
                        break;

                    case FilterType.Equals:
                        binaryExpression = !(type != typeof(string)) ? (!isCaseSensitive ? Expression.Equal((Expression)QueryableExtensions.GetToLowerMethodCallExpression(valueExpression), (Expression)Expression.Constant(value == null ? value : (object)value.ToString().ToLower(), valueExpression.Type)) : Expression.Equal(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type))) : Expression.Equal(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type));
                        break;

                    case FilterType.NotEquals:
                        binaryExpression = !(type != typeof(string)) ? (!isCaseSensitive ? Expression.NotEqual((Expression)QueryableExtensions.GetToLowerMethodCallExpression(valueExpression), (Expression)Expression.Constant(value == null ? value : (object)value.ToString().ToLower(), valueExpression.Type)) : Expression.NotEqual(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type))) : Expression.NotEqual(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type));
                        break;

                    case FilterType.GreaterThanOrEqual:
                        binaryExpression = Expression.GreaterThanOrEqual(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type));
                        break;

                    case FilterType.GreaterThan:
                        binaryExpression = Expression.GreaterThan(valueExpression, (Expression)Expression.Constant(value, valueExpression.Type));
                        break;
                }
                LambdaExpression lambdaExpression = Expression.Lambda((Expression)binaryExpression, paramExpression);
                return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Where), new Type[1]
                {
          source.ElementType
                }, source.Expression, (Expression)lambdaExpression));
            }
            MethodInfo method = ((IEnumerable<MethodInfo>)typeof(string).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m => m.Name == filterType.ToString())).FirstOrDefault<MethodInfo>();
            Expression body;
            if (isCaseSensitive)
                body = (Expression)Expression.Call(valueExpression, method, (Expression)Expression.Constant(value, typeof(string)));
            else
                body = (Expression)Expression.Call((Expression)QueryableExtensions.GetToLowerMethodCallExpression(valueExpression), method, (Expression)Expression.Constant(value == null ? value : (object)value.ToString().ToLower(), typeof(string)));
            LambdaExpression lambdaExpression1 = Expression.Lambda(body, paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Where), new Type[1]
            {
        source.ElementType
            }, source.Expression, (Expression)lambdaExpression1));
        }

        public static IQueryable Where(
          this IQueryable source,
          string propertyName,
          object value,
          FilterType filterType,
          bool isCaseSensitive)
        {
            Type elementType = source.ElementType;
            return source.Where(propertyName, value, filterType, isCaseSensitive, elementType);
        }

        /// <summary>
        /// Use this function to generate WHERE expression based on Predicates. The
        /// AndPredicate and OrPredicate should be used in combination to build the
        /// predicate expression which is finally passed on to this function for creating a
        /// Lambda.
        /// <para></para>
        /// <para></para>
        /// <para></para>DataClasses1DataContext db = new DataClasses1DataContext();
        /// <para></para>            var orders = db.Orders.Skip(0).Take(100).ToList();
        /// <para></para>            var queryable = orders.AsQueryable();
        /// <para></para>            var parameter =
        /// queryable.Parameter("ShipCountry");
        /// <para></para>            var binaryExp = queryable.Predicate(parameter,
        /// <para></para>"ShipCountry", "USA", true);
        /// <para></para>            var filteredOrders = queryable.Where(parameter,
        /// binaryExp);
        /// <para></para>            foreach (var order in filteredOrders)
        /// <para></para>            {
        /// <para></para>                Console.WriteLine(order);
        /// <para></para>            }
        /// <para></para>
        /// <para></para>
        /// <para></para>Build Predicates for Contains / StartsWith / EndsWith,
        /// <para></para>
        /// <para></para>            IQueryable queryable = nw.Orders.AsQueryable();
        /// <para></para>            var parameter = queryable.Parameter();
        /// <para></para>            var exp1 = queryable.Predicate(parameter,
        /// "ShipCountry", "h", FilterType.Contains);
        /// <para></para>            var exp2 = queryable.Predicate(parameter,
        /// "ShipCountry", "a", FilterType.StartsWith);
        /// <para></para>            var andExp = exp2.OrPredicate(exp1);
        /// <para></para>            var filters = queryable.Where(parameter, andExp);
        /// <para></para>            foreach (Orders item in filters)
        /// <para></para>            {
        /// <para></para>                Console.WriteLine("{0}/{1}",
        /// item.OrderID, item.ShipCountry);
        /// <para></para>            }
        /// <para></para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="paramExpression"></param>
        /// <param name="predicateExpression"></param>
        public static IQueryable Where(
          this IQueryable source,
          ParameterExpression paramExpression,
          Expression predicateExpression)
        {
            LambdaExpression lambdaExpression = Expression.Lambda(predicateExpression, paramExpression);
            return source.Provider.CreateQuery((Expression)Expression.Call(typeof(Queryable), nameof(Where), new Type[1]
            {
        source.ElementType
            }, source.Expression, (Expression)lambdaExpression));
        }

        public static IEnumerable<GroupResult> GroupByMany<TElement>(
          this IEnumerable<TElement> elements,
          List<SortDescription> sortFields,
          IEnumerable<Func<TElement, object>> groupSelectors)
        {
            return elements.GroupByMany<TElement>(sortFields, groupSelectors.ToArray<Func<TElement, object>>());
        }

        public static IEnumerable<GroupResult> GroupByMany<TElement>(
          this IEnumerable<TElement> elements,
          List<SortDescription> sortFields,
          Dictionary<string, IComparer<object>> sortComparers,
          string[] properties,
          IEnumerable<Func<TElement, object>> groupSelectors)
        {
            return elements.GroupByMany<TElement>(sortFields, sortComparers, Enumerable.ToList<string>((IEnumerable<string>)properties), groupSelectors.ToArray<Func<TElement, object>>());
        }

        public static IEnumerable<GroupResult> GroupByMany<TElement>(
          this IEnumerable<TElement> elements,
          List<SortDescription> sortFields,
          Dictionary<string, IComparer<object>> sortComparers,
          List<string> properties,
          params Func<TElement, object>[] groupSelectors)
        {
            if ((uint)groupSelectors.Length <= 0U)
                return (IEnumerable<GroupResult>)null;
            Func<TElement, object> keySelector = ((IEnumerable<Func<TElement, object>>)groupSelectors).First<Func<TElement, object>>();
            Func<TElement, object>[] nextSelectors = ((IEnumerable<Func<TElement, object>>)groupSelectors).Skip<Func<TElement, object>>(1).ToArray<Func<TElement, object>>();
            IEnumerable<GroupResult> source = elements.GroupBy<TElement, object>(keySelector).Select<IGrouping<object, TElement>, GroupResult>((Func<IGrouping<object, TElement>, GroupResult>)(g => new GroupResult()
            {
                Key = g.Key,
                Count = g.Count<TElement>(),
                Items = (IEnumerable)g,
                SubGroups = g.GroupByMany<TElement>(sortFields.Count > 0 ? Enumerable.ToList<SortDescription>(sortFields.Skip<SortDescription>(1)) : sortFields, sortComparers, properties.Count<string>() > 0 ? Enumerable.ToList<string>(properties.Skip<string>(0)) : properties, nextSelectors)
            }));
            if (sortFields.Count > 0)
            {
                SortDescription sortDescription = sortFields.Where<SortDescription>((Func<SortDescription, bool>)(d => d.PropertyName == properties[0])).FirstOrDefault<SortDescription>();
                if (sortDescription.PropertyName != null && sortDescription != new SortDescription())
                {
                    IComparer<object> comparer = (IComparer<object>)null;
                    sortComparers.TryGetValue(sortDescription.PropertyName, out comparer);
                    source = sortDescription.Direction != ListSortDirection.Ascending ? (comparer != null ? (IEnumerable<GroupResult>)source.OrderByDescending<GroupResult, object>((Func<GroupResult, object>)(g => g.Key), comparer) : (IEnumerable<GroupResult>)source.OrderByDescending<GroupResult, object>((Func<GroupResult, object>)(g => g.Key))) : (comparer != null ? (IEnumerable<GroupResult>)source.OrderBy<GroupResult, object>((Func<GroupResult, object>)(g => g.Key), comparer) : (IEnumerable<GroupResult>)source.OrderBy<GroupResult, object>((Func<GroupResult, object>)(g => g.Key)));
                }
            }
            return source;
        }

        public static IEnumerable<GroupResult> GroupByMany<TElement>(
          this IEnumerable<TElement> elements,
          List<SortDescription> sortFields,
          params Func<TElement, object>[] groupSelectors)
        {
            if ((uint)groupSelectors.Length <= 0U)
                return (IEnumerable<GroupResult>)null;
            Func<TElement, object> keySelector = ((IEnumerable<Func<TElement, object>>)groupSelectors).First<Func<TElement, object>>();
            Func<TElement, object>[] nextSelectors = ((IEnumerable<Func<TElement, object>>)groupSelectors).Skip<Func<TElement, object>>(1).ToArray<Func<TElement, object>>();
            IEnumerable<GroupResult> source = elements.GroupBy<TElement, object>(keySelector).Select<IGrouping<object, TElement>, GroupResult>((Func<IGrouping<object, TElement>, GroupResult>)(g => new GroupResult()
            {
                Key = g.Key,
                Count = g.Count<TElement>(),
                Items = (IEnumerable)g,
                SubGroups = g.GroupByMany<TElement>(sortFields.Count > 0 ? Enumerable.ToList<SortDescription>(sortFields.Skip<SortDescription>(1)) : sortFields, nextSelectors)
            }));
            if (sortFields.Count > 0)
            {
                SortDescription sortDescription = sortFields.First<SortDescription>();
                if (sortDescription.PropertyName != null)
                    source = sortDescription.Direction != ListSortDirection.Ascending ? (IEnumerable<GroupResult>)source.OrderByDescending<GroupResult, object>((Func<GroupResult, object>)(g => g.Key)) : (IEnumerable<GroupResult>)source.OrderBy<GroupResult, object>((Func<GroupResult, object>)(g => g.Key));
            }
            return source;
        }

        public static IEnumerable<GroupResult> GroupByMany<TElement>(
          this IEnumerable<TElement> elements,
          params Func<TElement, object>[] groupSelectors)
        {
            if ((uint)groupSelectors.Length <= 0U)
                return (IEnumerable<GroupResult>)null;
            Func<TElement, object> keySelector = ((IEnumerable<Func<TElement, object>>)groupSelectors).First<Func<TElement, object>>();
            Func<TElement, object>[] nextSelectors = ((IEnumerable<Func<TElement, object>>)groupSelectors).Skip<Func<TElement, object>>(1).ToArray<Func<TElement, object>>();
            return elements.GroupBy<TElement, object>(keySelector).Select<IGrouping<object, TElement>, GroupResult>((Func<IGrouping<object, TElement>, GroupResult>)(g => new GroupResult()
            {
                Key = g.Key,
                Count = g.Count<TElement>(),
                Items = (IEnumerable)g,
                SubGroups = g.GroupByMany<TElement>(nextSelectors)
            }));
        }

        public static IEnumerable<GroupResult> GroupByMany<TElement>(
          this IEnumerable<TElement> elements,
          IEnumerable<Func<TElement, object>> groupSelectors)
        {
            return elements.GroupByMany<TElement>(groupSelectors.ToArray<Func<TElement, object>>());
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IQueryable source,
          IEnumerable<string> properties)
        {
            return source.GroupByMany(properties.ToArray<string>());
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IQueryable source,
          Type sourceType,
          params string[] properties)
        {
            return source.GroupByMany((Dictionary<string, string>)null, sourceType, properties);
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IQueryable source,
          Dictionary<string, string> formatColl,
          Type sourceType,
          params string[] properties)
        {
            if (properties.Length == 0)
                return (IEnumerable<GroupResult>)null;
            string empty = string.Empty;
            List<LambdaExpression> lambdaExpressionList = new List<LambdaExpression>();
            foreach (string property1 in properties)
            {
                string property = property1;
                string str = string.Empty;
                ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
                Expression valueExpression = paramExpression.GetValueExpression(property, sourceType);
                UnaryExpression unaryExpression = Expression.Convert(valueExpression, typeof(object));
                if (formatColl != null)
                {
                    if (formatColl.Keys.Contains<string>(property))
                    {
                        str = Enumerable.ToList<KeyValuePair<string, string>>(formatColl.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(key => key.Key == property))).FirstOrDefault<KeyValuePair<string, string>>().Value;
                        if (str.Contains(property))
                            str = str.Replace(property, "0");
                        if (!string.IsNullOrEmpty(str))
                            str = Regex.Match(str, "{0:(.*?)}").Groups[1].Value;
                    }
                    if (!string.IsNullOrEmpty(str) && valueExpression.Type != typeof(string))
                        unaryExpression = Expression.Convert((Expression)QueryableExtensions.GetFormatMethodCallExpression(valueExpression, str), typeof(object));
                }
                LambdaExpression lambdaExpression = Expression.Lambda((Expression)unaryExpression, paramExpression);
                lambdaExpressionList.Add(lambdaExpression);
            }
            IList generic = QueryableExtensions.CreateGeneric(typeof(List<>), lambdaExpressionList[0].Type);
            foreach (LambdaExpression lambdaExpression in lambdaExpressionList)
                generic.Add((object)lambdaExpression.Compile());
            return QueryableExtensions.GetGroupByManyMethod().MakeGenericMethod(source.ElementType).Invoke((object)null, new object[2]
            {
        (object) source,
        (object) generic
            }) as IEnumerable<GroupResult>;
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IQueryable source,
          Type sourceType,
          List<SortDescription> sortFields,
          params string[] properties)
        {
            if (properties.Length == 0)
                return (IEnumerable<GroupResult>)null;
            List<LambdaExpression> lambdaExpressionList = new List<LambdaExpression>();
            foreach (string property in properties)
            {
                ParameterExpression paramExpression = Expression.Parameter(source.ElementType, sourceType.Name);
                LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Convert(paramExpression.GetValueExpression(property, sourceType), typeof(object)), paramExpression);
                lambdaExpressionList.Add(lambdaExpression);
            }
            IList generic = QueryableExtensions.CreateGeneric(typeof(List<>), lambdaExpressionList[0].Type);
            foreach (LambdaExpression lambdaExpression in lambdaExpressionList)
                generic.Add((object)lambdaExpression.Compile());
            return QueryableExtensions.GetGroupByManyMethod2().MakeGenericMethod(source.ElementType).Invoke((object)null, new object[3]
            {
        (object) source,
        (object) sortFields,
        (object) generic
            }) as IEnumerable<GroupResult>;
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IEnumerable source,
          Type sourceType,
          Func<string, Expression> GetExpressionFunc,
          params string[] properties)
        {
            if (properties.Length == 0)
                return (IEnumerable<GroupResult>)null;
            Type elementType = source.GetElementType();
            List<LambdaExpression> lambdaExpressionList = new List<LambdaExpression>();
            foreach (string property in properties)
            {
                ParameterExpression paramExpression = Expression.Parameter(elementType, sourceType.Name);
                Expression expression = GetExpressionFunc(property);
                if (expression == null)
                {
                    LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Convert(paramExpression.GetValueExpression(property, sourceType), typeof(object)), paramExpression);
                    lambdaExpressionList.Add(lambdaExpression);
                }
                else
                {
                    ConstantExpression constantExpression = Expression.Constant((object)property);
                    LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke(expression, (Expression)constantExpression, (Expression)paramExpression), paramExpression);
                    lambdaExpressionList.Add(lambdaExpression);
                }
            }
            IList generic = QueryableExtensions.CreateGeneric(typeof(List<>), lambdaExpressionList[0].Type);
            foreach (LambdaExpression lambdaExpression in lambdaExpressionList)
                generic.Add((object)lambdaExpression.Compile());
            return QueryableExtensions.GetGroupByManyMethod().MakeGenericMethod(source.GetElementType()).Invoke((object)null, new object[2]
            {
        (object) source,
        (object) generic
            }) as IEnumerable<GroupResult>;
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IEnumerable source,
          Type sourceType,
          List<SortDescription> sortFields,
          Dictionary<string, IComparer<object>> sortComparers,
          Func<string, Expression> GetExpressionFunc,
          params string[] properties)
        {
            if (properties.Length == 0)
                return (IEnumerable<GroupResult>)null;
            Type elementType = source.GetElementType();
            List<LambdaExpression> lambdaExpressionList = new List<LambdaExpression>();
            foreach (string property in properties)
            {
                ParameterExpression paramExpression = Expression.Parameter(elementType, sourceType.Name);
                Expression expression = GetExpressionFunc(property);
                if (expression == null)
                {
                    LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Convert(paramExpression.GetValueExpression(property, sourceType), typeof(object)), paramExpression);
                    lambdaExpressionList.Add(lambdaExpression);
                }
                else
                {
                    ConstantExpression constantExpression = Expression.Constant((object)property);
                    LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke(expression, (Expression)constantExpression, (Expression)paramExpression), paramExpression);
                    lambdaExpressionList.Add(lambdaExpression);
                }
            }
            IList generic = QueryableExtensions.CreateGeneric(typeof(List<>), lambdaExpressionList[0].Type);
            foreach (LambdaExpression lambdaExpression in lambdaExpressionList)
                generic.Add((object)lambdaExpression.Compile());
            return QueryableExtensions.GetGroupByManyMethod3().MakeGenericMethod(elementType).Invoke((object)null, new object[5]
            {
        (object) source,
        (object) sortFields,
        (object) sortComparers,
        (object) properties,
        (object) generic
            }) as IEnumerable<GroupResult>;
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IEnumerable source,
          Type sourceType,
          List<SortDescription> sortFields,
          Func<string, Expression> GetExpressionFunc,
          params string[] properties)
        {
            if (properties.Length == 0)
                return (IEnumerable<GroupResult>)null;
            Type elementType = source.GetElementType();
            List<LambdaExpression> lambdaExpressionList = new List<LambdaExpression>();
            foreach (string property in properties)
            {
                ParameterExpression paramExpression = Expression.Parameter(elementType, sourceType.Name);
                Expression expression = GetExpressionFunc(property);
                if (expression == null)
                {
                    LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Convert(paramExpression.GetValueExpression(property, sourceType), typeof(object)), paramExpression);
                    lambdaExpressionList.Add(lambdaExpression);
                }
                else
                {
                    ConstantExpression constantExpression = Expression.Constant((object)property);
                    LambdaExpression lambdaExpression = Expression.Lambda((Expression)Expression.Invoke(expression, (Expression)constantExpression, (Expression)paramExpression), paramExpression);
                    lambdaExpressionList.Add(lambdaExpression);
                }
            }
            IList generic = QueryableExtensions.CreateGeneric(typeof(List<>), lambdaExpressionList[0].Type);
            foreach (LambdaExpression lambdaExpression in lambdaExpressionList)
                generic.Add((object)lambdaExpression.Compile());
            return QueryableExtensions.GetGroupByManyMethod2().MakeGenericMethod(elementType).Invoke((object)null, new object[3]
            {
        (object) source,
        (object) sortFields,
        (object) generic
            }) as IEnumerable<GroupResult>;
        }

        private static MethodInfo GetGroupByManyMethod()
        {
            MethodInfo methodInfo1 = (MethodInfo)null;
            foreach (MethodInfo methodInfo2 in ((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == "GroupByMany" && m.IsStatic && m.IsPublic)
                 return m.IsGenericMethod;
             return false;
         })))
            {
                ParameterInfo[] parameters = methodInfo2.GetParameters();
                if (parameters.Length > 1 && typeof(IEnumerable<>).Name == parameters[1].ParameterType.Name)
                {
                    methodInfo1 = methodInfo2;
                    break;
                }
                if (methodInfo1 != (MethodInfo)null)
                    break;
            }
            return methodInfo1;
        }

        private static MethodInfo GetGroupByManyMethod2()
        {
            MethodInfo methodInfo1 = (MethodInfo)null;
            foreach (MethodInfo methodInfo2 in ((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == "GroupByMany" && m.IsStatic && m.IsPublic)
                 return m.IsGenericMethod;
             return false;
         })))
            {
                ParameterInfo[] parameters = methodInfo2.GetParameters();
                if (parameters.Length > 2 && typeof(IEnumerable<>).Name == parameters[2].ParameterType.Name)
                {
                    methodInfo1 = methodInfo2;
                    break;
                }
                if (methodInfo1 != (MethodInfo)null)
                    break;
            }
            return methodInfo1;
        }

        private static MethodInfo GetGroupByManyMethod3()
        {
            MethodInfo methodInfo1 = (MethodInfo)null;
            foreach (MethodInfo methodInfo2 in ((IEnumerable<MethodInfo>)typeof(QueryableExtensions).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>)(m =>
         {
             if (m.Name == "GroupByMany" && m.IsStatic && m.IsPublic)
                 return m.IsGenericMethod;
             return false;
         })))
            {
                ParameterInfo[] parameters = methodInfo2.GetParameters();
                if (parameters.Length > 3 && typeof(Dictionary<string, IComparer<object>>).Name == parameters[2].ParameterType.Name)
                {
                    methodInfo1 = methodInfo2;
                    break;
                }
                if (methodInfo1 != (MethodInfo)null)
                    break;
            }
            return methodInfo1;
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IQueryable source,
          params string[] properties)
        {
            Type elementType = source.ElementType;
            return source.GroupByMany(elementType, properties);
        }

        public static IEnumerable<GroupResult> GroupByMany(
          this IQueryable source,
          Dictionary<string, string> formatcoll,
          params string[] properties)
        {
            Type elementType = source.ElementType;
            return source.GroupByMany(formatcoll, elementType, properties);
        }

        private static IList CreateGeneric(Type generic, Type innerType, params object[] args)
        {
            return (IList)Activator.CreateInstance(generic.MakeGenericType(innerType), args);
        }

        public static Type GetObjectType(this IQueryable source)
        {
            Type type = source.ElementType;
            if (type == typeof(object))
            {
                IEnumerator enumerator = source.GetEnumerator();
                if (enumerator.MoveNext())
                    type = enumerator.Current.GetType();
            }
            return type;
        }

        internal static TypeInfo CreateClass(IEnumerable<DynamicProperty> properties)
        {
            return ClassFactory.Instance.GetDynamicClass(properties);
        }

        internal static TypeInfo CreateClass(params DynamicProperty[] properties)
        {
            return ClassFactory.Instance.GetDynamicClass((IEnumerable<DynamicProperty>)properties);
        }

        private static Expression GenerateNew(
          IEnumerable<string> properties,
          ParameterExpression paramExpression)
        {
            List<Expression> expressionList = new List<Expression>();
            List<DynamicProperty> dynamicPropertyList = new List<DynamicProperty>();
            foreach (string property in properties)
            {
                MemberExpression memberExpression = Expression.PropertyOrField((Expression)paramExpression, property);
                expressionList.Add((Expression)memberExpression);
                dynamicPropertyList.Add(new DynamicProperty(property, memberExpression.Type));
            }
            TypeInfo typeInfo = QueryableExtensions.CreateClass((IEnumerable<DynamicProperty>)dynamicPropertyList);
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            for (int index = 0; index < dynamicPropertyList.Count; ++index)
                memberBindingList.Add((MemberBinding)Expression.Bind((MemberInfo)typeInfo.GetDeclaredProperty(dynamicPropertyList[index].Name), expressionList[index]));
            return (Expression)Expression.MemberInit(Expression.New(Enumerable.ToList<ConstructorInfo>(typeInfo.DeclaredConstructors)[0]), (IEnumerable<MemberBinding>)memberBindingList);
        }
    }
}
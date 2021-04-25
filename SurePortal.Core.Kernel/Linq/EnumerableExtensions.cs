using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SurePortal.Core.Kernel.Linq
{
    public static class EnumerableExtensions
    {
        public static double Average<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static double Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short> selector)
        {
            return source.Select(selector).Average();
        }

        public static double Average(this IEnumerable<short> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            long num1 = 0;
            long num2 = 0;
            foreach (int num3 in source)
            {
                num1 += num3;
                ++num2;
            }

            if (num2 <= 0L)
                throw new InvalidOperationException("Not enough elements");
            return num1 / (double)num2;
        }

        public static double? Average<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short?>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            var nullable = source.Provider.Execute<short?>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
            return nullable.HasValue ? nullable.GetValueOrDefault() : new double?();
        }

        public static double? Average<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short?> selector)
        {
            return source.Select(selector).Average();
        }

        public static double? Average(this IEnumerable<short?> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            long num1 = 0;
            long num2 = 0;
            foreach (var nullable in source)
                if (nullable.HasValue)
                {
                    num1 += nullable.GetValueOrDefault();
                    ++num2;
                }

            if (num2 > 0L)
                return num1 / (double)num2;
            return new double?();
        }

        public static short Sum<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static short Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short> selector)
        {
            return source.Select(selector).Sum();
        }

        public static short Sum(this IEnumerable<short> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            short num1 = 0;
            foreach (var num2 in source)
                num1 += num2;
            return num1;
        }

        public static short? Sum<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short?>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short?>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static short? Sum<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short?> selector)
        {
            return source.Select(selector).Sum();
        }

        public static short? Sum(this IEnumerable<short?> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            short num = 0;
            foreach (var nullable in source)
                if (nullable.HasValue)
                    num += nullable.GetValueOrDefault();
            return num;
        }

        public static short Max<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static short Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short> selector)
        {
            return source.Select(selector).Max();
        }

        public static short Max(this IEnumerable<short> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            short num1 = 0;
            var flag = false;
            foreach (var num2 in source)
                if (flag)
                {
                    if (num2 > num1)
                        num1 = num2;
                }
                else
                {
                    num1 = num2;
                    flag = true;
                }

            if (!flag)
                throw new InvalidOperationException("Not enough elements");
            return num1;
        }

        public static short? Max<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short?>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short?>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static short? Max<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short?> selector)
        {
            return source.Select(selector).Max();
        }

        public static short? Max(this IEnumerable<short?> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var nullable1 = new short?();
            foreach (var nullable2 in source)
            {
                if (nullable1.HasValue)
                {
                    var nullable3 = nullable2;
                    var nullable4 = nullable1;
                    if (nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault() ||
                        !(nullable3.HasValue & nullable4.HasValue))
                        continue;
                }

                nullable1 = nullable2;
            }

            return nullable1;
        }

        public static short Min<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static short Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short> selector)
        {
            return source.Select(selector).Min();
        }

        public static short Min(this IEnumerable<short> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            short num1 = 0;
            var flag = false;
            foreach (var num2 in source)
                if (flag)
                {
                    if (num2 < num1)
                        num1 = num2;
                }
                else
                {
                    num1 = num2;
                    flag = true;
                }

            if (!flag)
                throw new InvalidOperationException("Not enough elements");
            return num1;
        }

        public static short? Min<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, short?>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            return source.Provider.Execute<short?>(Expression.Call(null,
                ((MethodInfo)MethodBase.GetMethodFromHandle(new RuntimeMethodHandle())).MakeGenericMethod(
                    typeof(TSource)), new Expression[2]
                {
                    source.Expression,
                    Expression.Quote(selector)
                }));
        }

        public static short? Min<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, short?> selector)
        {
            return source.Select(selector).Min();
        }

        public static short? Min(this IEnumerable<short?> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var nullable1 = new short?();
            foreach (var nullable2 in source)
            {
                if (nullable1.HasValue)
                {
                    var nullable3 = nullable2;
                    var nullable4 = nullable3.HasValue ? nullable3.GetValueOrDefault() : new long?();
                    nullable3 = nullable1;
                    var nullable5 = nullable3.HasValue ? nullable3.GetValueOrDefault() : new long?();
                    if (nullable4.GetValueOrDefault() >= nullable5.GetValueOrDefault() ||
                        !(nullable4.HasValue & nullable5.HasValue))
                        continue;
                }

                nullable1 = nullable2;
            }

            return nullable1;
        }

        public static Type GetElementType(this IEnumerable source)
        {
            var property = source.GetType().GetProperty("Item");
            return property != (PropertyInfo)null ? property.PropertyType : source.GetItemType(false);
        }

        internal static Type GetElementType(this IEnumerable source, ref bool isEmpty)
        {
            var representativeItem = GetRepresentativeItem(source);
            if (representativeItem != null)
            {
                var sourceType = CastToSourceType(source);
                isEmpty = false;
                var type = sourceType;
                if ((object)type == null)
                    type = representativeItem.GetType();
                return type;
            }

            var property = source.GetType().GetProperty("Item");
            isEmpty = true;
            return property != (PropertyInfo)null ? property.PropertyType : source.GetItemType(true);
        }

        internal static Type CastToSourceType(IEnumerable source)
        {
            return GetBaseGenericInterfaceType(source.GetType(), false);
        }

        private static Type GetBaseGenericInterfaceType(Type type, bool canreturn)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                if (genericArguments.Length == 1 && (genericArguments[0].GetTypeInfo().IsInterface ||
                                                     genericArguments[0].GetTypeInfo().IsAbstract || canreturn))
                    return genericArguments[0];
            }
            else if (type.GetTypeInfo().BaseType != null)
            {
                return GetBaseGenericInterfaceType(type.GetTypeInfo().BaseType, canreturn);
            }

            return null;
        }

        internal static Type GetItemType(this IEnumerable source, bool useRepresentativeItem)
        {
            var type = source.GetType();
            if (type.GetTypeInfo().IsGenericType)
            {
                var genericInterfaceType = GetBaseGenericInterfaceType(type, true);
                if ((genericInterfaceType == null || genericInterfaceType.GetTypeInfo().IsInterface ||
                     genericInterfaceType.GetTypeInfo().IsAbstract) && useRepresentativeItem)
                {
                    var representativeItem = GetRepresentativeItem(source);
                    if (representativeItem != null)
                        return representativeItem.GetType();
                }

                return genericInterfaceType;
            }

            if (useRepresentativeItem)
            {
                var representativeItem = GetRepresentativeItem(source);
                if (representativeItem != null)
                    return representativeItem.GetType();
                if (type.GetTypeInfo().BaseType != null && type.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType)
                    return type.GetTypeInfo().BaseType.GetGenericArguments()[0];
            }

            return null;
        }

        private static object GetRepresentativeItem(IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            if (enumerator.MoveNext())
                return enumerator.Current;
            return null;
        }
    }
}
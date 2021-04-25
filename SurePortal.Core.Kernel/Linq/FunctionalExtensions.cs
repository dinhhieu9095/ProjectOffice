using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace SurePortal.Core.Kernel.Linq
{
    /// <summary>Functional method extensions</summary>
    public static class FunctionalExtensions
    {
        public static T CreateNew<T>(this Type type)
        {
            return (T)type.CreateNew();
        }

        public static object CreateNew(this Type type)
        {
            var parameterExpression = Expression.Parameter(type, type.Name);
            return Expression
                .Lambda(Expression.MemberInit(Expression.New(type), new List<MemberBinding>()), parameterExpression)
                .Compile().DynamicInvoke(new object[1]);
        }

        /// <summary>
        ///     Iterates over an IEnumerable instance to a delegated function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable items, Action<T> action)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            foreach (var obj in items)
                action((T)obj);
        }

        /// <summary>
        ///     Iterates over a generic IEnumerable instance to a delegated function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            foreach (var obj in items)
                action(obj);
        }

        public static IEnumerable<T> ToList<T>(this IEnumerable items)
        {
            foreach (var obj in items)
            {
                var item = obj;
                yield return (T)item;
                item = null;
            }
        }

        /// <summary>Iterates the index.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void IterateIndex<T>(this T[] items, Action<int, T> action)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            var lowerBound = items.GetLowerBound(0);
            var upperBound = items.GetUpperBound(0);
            for (var index = lowerBound; index < upperBound; ++index)
                action(index, items[index]);
        }

        /// <summary>Iterates the index.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void IterateIndex<T>(this IEnumerable<T> items, Action<int, T> action)
        {
            items.IterateIndex(action, 0);
        }

        public static void IterateIndex<T>(this IEnumerable<T> items, Action<int, T> action, int idx)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            var enumerator = items.GetEnumerator();
            for (var index = 0; index < idx; ++index)
                enumerator.MoveNext();
            while (enumerator.MoveNext())
            {
                action(idx, enumerator.Current);
                ++idx;
            }
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));
            return ZipIterator(first, second, resultSelector);
        }

        private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(
            IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            using (var e1 = first.GetEnumerator())
            {
                using (var e2 = second.GetEnumerator())
                {
                    while (e1.MoveNext() && e2.MoveNext())
                        yield return resultSelector(e1.Current, e2.Current);
                }
            }
        }

        /// <summary>Trues this instance.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        /// <summary>Falses this instance.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        /// <summary>Ors the specified expr1.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1">The expr1.</param>
        /// <param name="expr2">The expr2.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invocationExpression = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, invocationExpression), expr1.Parameters);
        }

        /// <summary>Ands the specified expr1.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1">The expr1.</param>
        /// <param name="expr2">The expr2.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invocationExpression = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invocationExpression), expr1.Parameters);
        }

        /// <summary>Folds the specified list.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <param name="acc">The acc.</param>
        /// <returns></returns>
        public static T Fold<T, U>(this IEnumerable<U> list, Func<T, U, T> func, T acc)
        {
            foreach (var u in list)
                acc = func(acc, u);
            return acc;
        }

        /// <summary>Folds the left.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <param name="acc">The acc.</param>
        /// <returns></returns>
        public static T FoldLeft<T, U>(this IEnumerable<U> list, Func<T, U, T> func, T acc)
        {
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
                acc = func(acc, enumerator.Current);
            return acc;
        }

        /// <summary>Folds the right.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <param name="acc">The acc.</param>
        /// <returns></returns>
        public static T FoldRight<T, U>(this IEnumerable<U> list, Func<T, U, T> func, T acc)
        {
            var enumerator = list.GetEnumerator();
            var uList = new List<U>();
            while (enumerator.MoveNext())
                uList.Add(enumerator.Current);
            for (var index = uList.Count - 1; index >= 0; --index)
                acc = func(acc, uList[index]);
            return acc;
        }

        /// <summary>Moves to.</summary>
        /// <param name="list">The list.</param>
        /// <param name="src">The SRC.</param>
        /// <param name="dest">The dest.</param>
        public static void MoveTo(this IList list, int src, int dest)
        {
            var obj = list[src];
            list.RemoveAt(src);
            list.Insert(dest, obj);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(
            this IEnumerable<T> items)
        {
            var observableCollection = new ObservableCollection<T>();
            foreach (var obj in items)
                observableCollection.Add(obj);
            return observableCollection;
        }
    }
}
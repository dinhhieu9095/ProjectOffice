using DaiPhatDat.Core.Kernel.JavaScript.Models;
using DaiPhatDat.Core.Kernel.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DaiPhatDat.Core.Kernel.JavaScript.DataSources
{
    public static class QueryableDataOperations
    {
        private static string condition = string.Empty;
        //private static Expression filterPredicate = null;

        private static Type DataSourceType<T>(IQueryable<T> dataSource)
        {
            var elementType = dataSource.GetElementType();
            if (elementType == null)
                elementType = dataSource.GetType().GetElementType();
            return elementType;
        }

        public static IQueryable<T> Execute<T>(this IQueryable<T> dataSource, DataManager manager)
        {
            dataSource = dataSource.ExecuteQuery(manager);
            if ((uint)manager.Skip > 0U)
                dataSource = dataSource.PerformSkip(manager.Skip);
            if ((uint)manager.Take > 0U)
                dataSource = dataSource.PerformTake(manager.Take);
            return dataSource;
        }

        public static int ExecuteCount<T>(this IQueryable<T> dataSource, DataManager manager)
        {
            return dataSource.ExecuteQuery(manager).Count();
        }

        public static IQueryable<T> ExecuteQuery<T>(this IQueryable<T> dataSource, DataManager manager)
        {
            if (manager.Where != null && manager.Where.Count > 0)
                dataSource = dataSource.PerformWhereFilter(manager.Where, string.Empty);
            if (manager.Search != null && manager.Search.Count > 0)
                dataSource = PerformSearching(dataSource, manager.Search);
            if (manager.Sorted != null && manager.Sorted.Count > 0)
                dataSource = dataSource.PerformSorting(manager.Sorted);
            return dataSource;
        }

        public static IQueryable PerformGrouping<T>(
            this IQueryable<T> dataSource,
            List<string> grouped)
        {
            grouped.ToArray<string>();
            DataSourceType(dataSource);
            return dataSource.GroupByMany(grouped).AsQueryable();
        }

        private static IOrderedQueryable<T> OrderingHelper<T>(
            this IQueryable<T> source,
            string propertyName,
            string operation)
        {
            var parameterExpression = Expression.Parameter(typeof(T), string.Empty);
            var memberExpression = Expression.PropertyOrField(parameterExpression, propertyName);
            var lambdaExpression = Expression.Lambda(memberExpression, parameterExpression);
            var methodCallExpression = Expression.Call(typeof(Queryable), operation, new Type[2]
            {
                typeof(T),
                memberExpression.Type
            }, source.Expression, (Expression)Expression.Quote(lambdaExpression));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(methodCallExpression);
        }

        private static Expression PerformComplexExpression(
            ParameterExpression param,
            string select)
        {
            var expression = (Expression)param;
            var strArray = select.Split('.');
            for (var index = 0; index < strArray.Length; ++index)
            {
                int result;
                if (int.TryParse(strArray[index], out result))
                {
                    var int16 = (int)Convert.ToInt16(strArray[index]);
                    if (index + 1 <= strArray.Length - 1)
                    {
                        expression = Expression.PropertyOrField(
                            Expression.ArrayIndex(expression, Expression.Constant(int16)), strArray[index + 1]);
                        ++index;
                    }
                    else
                    {
                        expression = Expression.ArrayIndex(expression, Expression.Constant(int16));
                    }
                }
                else
                {
                    expression = Expression.PropertyOrField(expression, strArray[index]);
                }
            }

            return expression;
        }

        private static IQueryable PerformComplexDataOperation<T>(
            this IQueryable<T> dataSource,
            string select)
        {
            var param = Expression.Parameter(typeof(T), "a");
            var property = PerformComplexExpression(param, select);
            var columnType = dataSource.GetColumnType(select, typeof(T));
            if (columnType.Name.ToLower().Equals("int32"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, int>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("uint32"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, uint>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("int64"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, long>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("uint64"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, ulong>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("int16"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, short>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("uint16"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, ushort>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("string"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, string>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("double"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, double>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("single") || columnType.Name.ToLower().Equals("float"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, float>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("char"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, char>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("bool"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, bool>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("byte"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, byte>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("sbyte"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, sbyte>>(property, param)).ToList()[i]
                });
            if (columnType.Name.ToLower().Equals("decimal"))
                return dataSource.Select((a, i) => new
                {
                    a,
                    TempData = dataSource.Select(Expression.Lambda<Func<T, decimal>>(property, param)).ToList()[i]
                });
            return dataSource.Select((a, i) => new
            {
                a,
                TempData = dataSource.Select(Expression.Lambda<Func<T, object>>(property, param)).ToList()[i]
            });
        }

        internal static IQueryable performComplexSorting<T>(
            this IQueryable<T> dataSource,
            string sortedColumns,
            bool firstTime,
            SortOrder sort)
        {
            var source = dataSource.PerformComplexDataOperation(sortedColumns);
            var objectType = source.GetObjectType();
            return (sort != SortOrder.Ascending ? firstTime ? source.OrderByDescending("TempData", objectType) :
                    source.ThenByDescending("TempData", objectType) :
                    firstTime ? source.OrderBy("TempData", objectType) : source.ThenBy("TempData", objectType))
                .Select("a", objectType);
        }

        public static IQueryable<T> PerformSorting<T>(
            this IQueryable<T> dataSource,
            List<SortedColumn> sortedColumns)
        {
            var firstTime = true;
            dataSource.GetObjectType();
            foreach (var sortedColumn in sortedColumns)
                if (sortedColumn.Field.Contains('.'))
                {
                    dataSource = dataSource.performComplexSorting(sortedColumn.Field, firstTime, sortedColumn.Direction)
                        .Cast<T>();
                    firstTime = false;
                }
                else if (sortedColumn.Direction == SortOrder.Ascending)
                {
                    if (firstTime)
                    {
                        dataSource = dataSource.OrderingHelper(sortedColumn.Field, "OrderBy");
                        firstTime = false;
                    }
                    else
                    {
                        dataSource = dataSource.OrderingHelper(sortedColumn.Field, "ThenBy");
                    }
                }
                else if (firstTime)
                {
                    dataSource = dataSource.OrderingHelper(sortedColumn.Field, "OrderByDescending");
                    firstTime = false;
                }
                else
                {
                    dataSource = dataSource.OrderingHelper(sortedColumn.Field, "ThenByDescending");
                }

            return dataSource;
        }

        public static IQueryable<T> PerformSorting<T>(
            this IQueryable<T> dataSource,
            List<Sort> sortedColumns)
        {
            var sortedColumns1 = new List<SortedColumn>();
            if (sortedColumns.Count > 1)
                sortedColumns.Reverse();
            foreach (var sortedColumn in sortedColumns)
            {
                var sortOrder = (SortOrder)Enum.Parse(typeof(SortOrder), sortedColumn.Direction, true);
                sortedColumns1.Add(new SortedColumn
                {
                    Direction = sortOrder,
                    Field = sortedColumn.Name
                });
            }

            dataSource = dataSource.PerformSorting(sortedColumns1);
            return dataSource;
        }

        public static IQueryable<T> PerformSearching<T>(
            IQueryable<T> dataSource,
            List<SearchFilter> searchFilter)
        {
            var type1 = DataSourceType(dataSource);
            var type2 = typeof(object);
            foreach (var searchFilter1 in searchFilter)
            {
                var paramExpression = type1.Parameter();
                var flag = true;
                var expression = (Expression)null;
                var filterType = (FilterType)Enum.Parse(typeof(FilterType),
                    searchFilter1.Operator == "equal" ? "equals" :
                    searchFilter1.Operator == "notequal" ? "notequals" : searchFilter1.Operator, true);
                foreach (var field in searchFilter1.Fields)
                {
                    type1 = dataSource.GetDataType(type1, field);
                    type2 = dataSource.GetColumnType(field, type1);
                    if (flag)
                    {
                        expression = dataSource.Predicate(paramExpression, field, searchFilter1.Key, filterType,
                            FilterBehavior.StringTyped, false, type1);
                        flag = false;
                    }
                    else
                    {
                        expression = expression.OrPredicate(dataSource.Predicate(paramExpression, field,
                            searchFilter1.Key, filterType, FilterBehavior.StringTyped, false, type1));
                    }
                }

                dataSource = dataSource.Where(Expression.Lambda<Func<T, bool>>(expression, paramExpression));
            }

            return dataSource;
        }

        private static Type GetDataType<T>(this IQueryable<T> dataSource, Type type, string field)
        {
            var strArray = field.Split('.');
            if (type.GetProperty(strArray[0]) == null)
                type = dataSource.GetObjectType();
            return type;
        }

        private static Type GetColumnType<T>(
            this IQueryable<T> dataSource,
            string filterString,
            Type type)
        {
            var strArray = filterString.Split('.');
            var propertyInfo = (PropertyInfo)null;
            for (var index = 0; index < strArray.Length; ++index)
            {
                int result;
                if (int.TryParse(strArray[index], out result))
                {
                    type = type.GetElementType();
                }
                else
                {
                    propertyInfo = type.GetProperty(strArray[index],
                        BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                    type = propertyInfo.PropertyType;
                }
            }

            return propertyInfo.PropertyType;
        }

        private static Type GetTableColumnType<T>(
            this IQueryable<T> dataSource,
            string filterString,
            Type type)
        {
            filterString.Split('.');
            var type1 = typeof(object);
            var flag = type.Name == "DataRow";
            return type1;
        }

        public static IQueryable<T> PerformSkip<T>(this IQueryable<T> dataSource, int skip)
        {
            return dataSource.Skip(skip);
        }

        public static IQueryable<T> PerformTake<T>(this IQueryable<T> dataSource, int take)
        {
            return dataSource.Take(take);
        }

        public static IQueryable<T> PerformWhereFilter<T>(
            this IQueryable<T> dataSource,
            List<WhereFilter> whereFilter,
            string condition)
        {
            var queryable = (IQueryable<T>)null;
            var type1 = DataSourceType(dataSource);
            var type2 = typeof(object);
            var paramExpression = type1.Parameter();
            var expression = (Expression)null;
            foreach (var whereFilter1 in whereFilter)
                if (whereFilter1.IsComplex)
                {
                    dataSource = dataSource.PerformWhereFilter(whereFilter1.Predicates, whereFilter1.Condition);
                }
                else
                {
                    var filterType = (FilterType)Enum.Parse(typeof(FilterType),
                        whereFilter1.Operator == "equal" ? "equals" :
                        whereFilter1.Operator == "notequal" ? "notequals" : whereFilter1.Operator, true);
                    type1 = dataSource.GetDataType(type1, whereFilter1.Field);
                    var type3 = dataSource.GetColumnType(whereFilter1.Field, type1);
                    var underlyingType = Nullable.GetUnderlyingType(type3);
                    if (underlyingType != null)
                        type3 = underlyingType;
                    var constValue = whereFilter1.Value == null
                        ? whereFilter1.Value
                        : ChangeType(whereFilter1.Value, type3);
                    if (expression != null && condition == "or")
                    {
                        expression = expression.OrPredicate(dataSource.Predicate(paramExpression, whereFilter1.Field,
                            constValue, filterType, FilterBehavior.StringTyped, !whereFilter1.IgnoreCase, type1));
                        queryable = dataSource.Where(Expression.Lambda<Func<T, bool>>(expression, paramExpression));
                    }
                    else if (condition != "or")
                    {
                        expression = dataSource.Predicate(paramExpression, whereFilter1.Field, constValue, filterType,
                            FilterBehavior.StringTyped, !whereFilter1.IgnoreCase, type1);
                        dataSource = dataSource.Where(Expression.Lambda<Func<T, bool>>(expression, paramExpression));
                    }
                    else
                    {
                        expression = dataSource.Predicate(paramExpression, whereFilter1.Field, constValue, filterType,
                            FilterBehavior.StringTyped, !whereFilter1.IgnoreCase, type1);
                    }
                }

            if (queryable != null)
                dataSource = queryable;
            return dataSource;
        }
        public static object ChangeType(object value, Type type)
        {
            if (type == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }

            if (type.BaseType == typeof(Enum))
            {
                return Enum.Parse(type, value.ToString());
            }

            return Convert.ChangeType(value, type);
        }

        public static IQueryable PerformSelect<T>(
            this IQueryable<T> dataSource,
            List<string> select)
        {
            var properties = select.Where(item => item != null);
            return dataSource.Select(properties, dataSource.GetObjectType());
        }
    }
}
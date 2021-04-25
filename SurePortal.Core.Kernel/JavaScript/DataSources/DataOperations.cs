using SurePortal.Core.Kernel.JavaScript.Models;
using SurePortal.Core.Kernel.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SurePortal.Core.Kernel.JavaScript.DataSources
{
    public class DataOperations
    {
        private static string condition = string.Empty;
        //private Expression filterPredicate = null;

        public IEnumerable Execute(IEnumerable dataSource, DataManager manager)
        {
            dataSource = ExecuteQuery(dataSource, manager);
            if ((uint)manager.Skip > 0U)
                dataSource = PerformSkip(dataSource, manager.Skip);
            if ((uint)manager.Take > 0U)
                dataSource = PerformTake(dataSource, manager.Take);
            return dataSource;
        }

        public IEnumerable ExecuteQuery(IEnumerable dataSource, DataManager manager)
        {
            if (manager.Where != null && manager.Where.Count > 0)
                dataSource = PerformWhereFilter(dataSource, manager.Where, string.Empty);
            if (manager.Search != null && manager.Search.Count > 0)
                dataSource = PerformSearching(dataSource, manager.Search);
            if (manager.Sorted != null && manager.Sorted.Count > 0)
                dataSource = PerformSorting(dataSource, manager.Sorted);
            if (manager.Select != null && manager.Select.Count > 0)
                dataSource = PerformSelect(dataSource, manager.Select);
            return dataSource;
        }

        public IEnumerable<T> Execute<T>(IEnumerable<T> dataSource, DataManager manager)
        {
            return QueryableDataOperations.Execute(dataSource.AsQueryable(), manager);
        }

        public IEnumerable<T> ExecuteQuery<T>(IEnumerable<T> dataSource, DataManager manager)
        {
            return QueryableDataOperations.ExecuteQuery(dataSource.AsQueryable(), manager);
        }

        public int ExecuteCount<T>(IEnumerable<T> dataSource, DataManager manager)
        {
            return ExecuteQuery(dataSource, manager).Count();
        }

        public IEnumerable PerformGrouping(IEnumerable dataSource, List<string> grouped)
        {
            if (dataSource.GetElementType() == null)
                dataSource.GetType().GetElementType();
            grouped.ToArray<string>();
            dataSource = dataSource.AsQueryable().GroupByMany(grouped).AsQueryable();
            return dataSource;
        }

        public IEnumerable PerformGrouping<T>(
            IEnumerable<T> dataSource,
            List<string> grouped)
        {
            return dataSource.AsQueryable().PerformGrouping(grouped);
        }

        public IEnumerable<T> PerformSorting<T>(
            IEnumerable<T> dataSource,
            List<SortedColumn> sortedColumns)
        {
            return dataSource.AsQueryable().PerformSorting(sortedColumns);
        }

        public IEnumerable<T> PerformSorting<T>(
            IEnumerable<T> dataSource,
            List<Sort> sortedColumns)
        {
            return dataSource.AsQueryable().PerformSorting(sortedColumns);
        }

        public IEnumerable PerformSelect<T>(IEnumerable<T> dataSource, List<string> select)
        {
            return dataSource.AsQueryable().PerformSelect(select);
        }

        public IEnumerable<T> PerformSearching<T>(
            IEnumerable<T> dataSource,
            List<SearchFilter> searchFilter)
        {
            return QueryableDataOperations.PerformSearching(dataSource.AsQueryable(), searchFilter);
        }

        public IEnumerable<T> PerformSkip<T>(IEnumerable<T> dataSource, int skip)
        {
            return dataSource.AsQueryable().PerformSkip(skip);
        }

        public IEnumerable<T> PerformTake<T>(IEnumerable<T> dataSource, int take)
        {
            return dataSource.AsQueryable().PerformTake(take);
        }

        public IEnumerable<T> PerformWhereFilter<T>(
            IEnumerable<T> dataSource,
            List<WhereFilter> whereFilter,
            string condition)
        {
            return dataSource.AsQueryable().PerformWhereFilter(whereFilter, condition);
        }

        public IEnumerable PerformSorting(
            IEnumerable dataSource,
            List<SortedColumn> sortedColumns)
        {
            var flag = true;
            var source = dataSource.AsQueryable();
            var objectType = source.GetObjectType();
            foreach (var sortedColumn in sortedColumns)
                if (sortedColumn.Direction == SortOrder.Ascending)
                {
                    if (flag)
                    {
                        source = source.OrderBy(sortedColumn.Field, objectType);
                        flag = false;
                    }
                    else
                    {
                        source = source.ThenBy(sortedColumn.Field, objectType);
                    }
                }
                else if (flag)
                {
                    source = source.OrderByDescending(sortedColumn.Field, objectType);
                    flag = false;
                }
                else
                {
                    source = source.ThenByDescending(sortedColumn.Field, objectType);
                }

            return source;
        }

        public IEnumerable PerformSorting(IEnumerable dataSource, List<Sort> sortedColumns)
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

            dataSource = PerformSorting(dataSource, sortedColumns1);
            return dataSource;
        }

        private bool CheckGuid(string GuidValue)
        {
            var regex = new Regex(
                "^(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}$",
                RegexOptions.Compiled);
            var flag = false;
            var guid = Guid.Empty;
            if (GuidValue != null && regex.IsMatch(GuidValue))
            {
                guid = new Guid(GuidValue);
                flag = true;
            }

            return flag;
        }

        public IEnumerable PerformSearching(
            IEnumerable dataSource,
            List<SearchFilter> searchFilter)
        {
            var type1 = dataSource.GetElementType();
            var type2 = typeof(object);
            if (type1 == null)
                type1 = dataSource.GetType().GetElementType();
            foreach (var searchFilter1 in searchFilter)
            {
                var paramExpression = type1.Parameter();
                var flag = true;
                var expression = (Expression)null;
                var str = searchFilter1.Operator;
                if (str == "equal")
                    str = "equals";
                else if (str == "notequal")
                    str = "notequals";
                var filterType = (FilterType)Enum.Parse(typeof(FilterType), str, true);
                foreach (var field in searchFilter1.Fields)
                {
                    type1 = GetDataType(dataSource, type1, field);
                    type2 = GetColumnType(dataSource, field, type1);
                    if (flag)
                    {
                        expression = dataSource.AsQueryable().Predicate(paramExpression, field, searchFilter1.Key,
                            filterType, FilterBehavior.StringTyped, false, type1);
                        flag = false;
                    }
                    else
                    {
                        expression = expression.OrPredicate(dataSource.AsQueryable().Predicate(paramExpression, field,
                            searchFilter1.Key, filterType, FilterBehavior.StringTyped, false, type1));
                    }
                }

                dataSource = dataSource.AsQueryable().Where(paramExpression, expression);
            }

            return dataSource;
        }

        public Type GetDataType(IEnumerable dataSource, Type type, string field)
        {
            var strArray = field.Split('.');
            if (type.GetProperty(strArray[0]) == null)
                type = dataSource.AsQueryable().GetObjectType();
            return type;
        }

        public Type GetColumnType(IEnumerable dataSource, string filterString, Type type)
        {
            var strArray = filterString.Split('.');
            var property = type.GetProperty(strArray[0],
                BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (property == null)
                return null;
            for (var index = 1; index < strArray.Count(); ++index)
                property = property.PropertyType.GetProperty(strArray[index],
                    BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            return property.PropertyType;
        }

        public Type GetTableColumnType(IEnumerable dataSource, string filterString, Type type)
        {
            filterString.Split('.');
            var type1 = typeof(object);
            var flag = type.Name == "DataRow";
            return type1;
        }

        public IEnumerable PerformSkip(IEnumerable dataSource, int skip)
        {
            return dataSource.AsQueryable().Skip(skip);
        }

        public IEnumerable PerformTake(IEnumerable dataSource, int take)
        {
            return dataSource.AsQueryable().Take(take);
        }

        public IEnumerable PerformWhereFilter(
            IEnumerable dataSource,
            List<WhereFilter> whereFilter,
            string condition)
        {
            var enumerable = (IEnumerable)null;
            var type1 = dataSource.GetElementType();
            if (type1 == null)
                type1 = dataSource.GetType().GetElementType();
            var type2 = typeof(object);
            var paramExpression = type1.Parameter();
            var expression = (Expression)null;
            foreach (var whereFilter1 in whereFilter)
                if (whereFilter1.IsComplex)
                {
                    dataSource = PerformWhereFilter(dataSource, whereFilter1.Predicates, whereFilter1.Condition);
                }
                else
                {
                    var str = whereFilter1.Operator;
                    if (str == "equal")
                        str = "equals";
                    else if (str == "notequal")
                        str = "notequals";
                    var filterType = (FilterType)Enum.Parse(typeof(FilterType), str, true);
                    type1 = GetDataType(dataSource, type1, whereFilter1.Field);
                    var type3 = GetColumnType(dataSource, whereFilter1.Field, type1);
                    var underlyingType = Nullable.GetUnderlyingType(type3);
                    if (underlyingType != null)
                        type3 = underlyingType;
                    var constValue = whereFilter1.Value == null
                        ? whereFilter1.Value
                        : Convert.ChangeType(whereFilter1.Value, type3);
                    if (expression != null && condition == "or")
                    {
                        expression = expression.OrPredicate(dataSource.AsQueryable().Predicate(paramExpression,
                            whereFilter1.Field, constValue, filterType, FilterBehavior.StringTyped,
                            !whereFilter1.IgnoreCase, type1));
                        enumerable = dataSource.AsQueryable().Where(paramExpression, expression);
                    }
                    else if (condition != "or")
                    {
                        expression = dataSource.AsQueryable().Predicate(paramExpression, whereFilter1.Field, constValue,
                            filterType, FilterBehavior.StringTyped, !whereFilter1.IgnoreCase, type1);
                        dataSource = dataSource.AsQueryable().Where(paramExpression, expression);
                    }
                    else
                    {
                        expression = dataSource.AsQueryable().Predicate(paramExpression, whereFilter1.Field, constValue,
                            filterType, FilterBehavior.StringTyped, !whereFilter1.IgnoreCase, type1);
                    }
                }

            if (enumerable != null)
                dataSource = enumerable;
            return dataSource;
        }

        public IEnumerable PerformSelect(IEnumerable dataSource, List<string> select)
        {
            var properties = select.Where(item => item != null);
            var objectType = dataSource.AsQueryable().GetObjectType();
            dataSource = dataSource.AsQueryable().Select(properties, objectType);
            return dataSource;
        }

        public object GetSummaryValue(IQueryable items, SummaryType type, string field)
        {
            var obj = (object)"";
            //var num = new decimal();
            var elementType = items.ElementType;
            switch (type)
            {
                case SummaryType.Average:
                    obj = items.Average(field, elementType);
                    break;

                case SummaryType.Minimum:
                    obj = items.Min(field, elementType);
                    break;

                case SummaryType.Maximum:
                    obj = items.Max(field, elementType);
                    break;

                case SummaryType.Count:
                    obj = items.Count();
                    break;

                case SummaryType.Sum:
                    obj = items.Sum(field, elementType);
                    break;

                case SummaryType.Truecount:
                    obj = items.Where(field, true, FilterType.Equals, false, elementType).Count();
                    break;

                case SummaryType.Falsecount:
                    obj = items.Where(field, false, FilterType.Equals, false, elementType).Count();
                    break;
            }

            return obj;
        }
    }
}
using System.Linq;
using System.Text;

namespace DaiPhatDat.Core.Kernel.Linq.Data
{
    public static class DataSetExtensions
    {
        public static string Predicate(
            this string thisString,
            string property,
            object value,
            FilterType filterType)
        {
            if (property.Contains(']'))
                property = property.Replace("]", "\\]");
            switch (filterType)
            {
                case FilterType.LessThan:
                    return thisString += LessThan(property, value);

                case FilterType.LessThanOrEqual:
                    return thisString += LessThanOrEquals(property, value);

                case FilterType.Equals:
                    return thisString += Equals(property, value);

                case FilterType.NotEquals:
                    return thisString += NotEquals(property, value);

                case FilterType.GreaterThanOrEqual:
                    return thisString += GreaterThanOrEquals(property, value);

                case FilterType.GreaterThan:
                    return thisString += GreaterThan(property, value);

                case FilterType.StartsWith:
                    return thisString += StartsWith(property, value);

                case FilterType.EndsWith:
                    return thisString += EndsWith(property, value);

                case FilterType.Contains:
                    return thisString += Contains(property, value);

                default:
                    return string.Empty;
            }
        }

        public static string AndPredicate(this string thisString)
        {
            return thisString += " AND ";
        }

        public static string OrPredicate(this string thisString)
        {
            return thisString += " OR ";
        }

        private static string Equals(string property, object value)
        {
            return string.Format("[{0}] = '{1}'", property, value != null ? value.ToString() : string.Empty);
        }

        private static string NotEquals(string property, object value)
        {
            return string.Format("ISNULL([{0}],'Null Column') <> '{1}'", property,
                value != null ? value.ToString() : string.Empty);
        }

        private static string GreaterThan(string property, object value)
        {
            return string.Format("[{0}] > '{1}'", property, value != null ? value.ToString() : string.Empty);
        }

        private static string LessThan(string property, object value)
        {
            return string.Format("[{0}] < '{1}'", property, value != null ? value.ToString() : string.Empty);
        }

        private static string GreaterThanOrEquals(string property, object value)
        {
            return string.Format("[{0}] >= '{1}'", property, value != null ? value.ToString() : string.Empty);
        }

        private static string LessThanOrEquals(string property, object value)
        {
            return string.Format("[{0}] <= '{1}'", property, value != null ? value.ToString() : string.Empty);
        }

        private static string StartsWith(string property, object value)
        {
            return string.Format("Convert([{0}], 'System.String') LIKE '{1}*'", property,
                value != null ? EscapeLikeValue(value.ToString()) : string.Empty);
        }

        private static string EndsWith(string property, object value)
        {
            return string.Format("Convert([{0}], 'System.String') LIKE '*{1}'", property,
                value != null ? EscapeLikeValue(value.ToString()) : string.Empty);
        }

        private static string Contains(string property, object value)
        {
            return string.Format("Convert([{0}], 'System.String') LIKE '%{1}%'", property,
                value != null ? EscapeLikeValue(value.ToString()) : string.Empty);
        }

        public static string OrderBy(this string thisString, string propertyName)
        {
            return thisString += string.Format("[{0}] ASC", propertyName);
        }

        public static string OrderByDescending(this string thisString, string propertyName)
        {
            return thisString += string.Format("[{0}] DESC", propertyName);
        }

        public static string ThenBy(this string thisString, string propertyName)
        {
            return thisString += string.Format(",[{0}] ASC", propertyName);
        }

        public static string ThenByDescending(this string thisString, string propertyName)
        {
            return thisString += string.Format(",[{0}] DESC", propertyName);
        }

        /// <summary>
        ///     Insert [ wildcard ] in LIKE Queries.
        ///     http://msdn.microsoft.com/en-us/library/ms179859.aspx
        /// </summary>
        private static string EscapeLikeValue(string property)
        {
            var stringBuilder = new StringBuilder();
            for (var index = 0; index < property.Length; ++index)
            {
                var ch = property[index];
                int num;
                switch (ch)
                {
                    case '%':
                    case '*':
                    case '[':
                    case ']':
                        num = 1;
                        break;

                    default:
                        num = ch == '_' ? 1 : 0;
                        break;
                }

                if (num != 0)
                    stringBuilder.Append("[").Append(ch).Append("]");
                else if (ch == '\'')
                    stringBuilder.Append("''");
                else
                    stringBuilder.Append(ch);
            }

            return stringBuilder.ToString();
        }
    }
}
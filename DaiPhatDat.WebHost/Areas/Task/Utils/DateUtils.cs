using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Web.Utils
{
    public static class DateUtils
    {
        public static DateTime? ToDate(string dateText)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            bool valid = DateTime.TryParseExact(dateText, "dd/MM/yyyy", enUS,
                              DateTimeStyles.None, out DateTime result);
            if (valid)
            {
                return result;
            }
            return null;
        }
        public static DateTime ToMaxDate(this DateTime Date)
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 00);
        }

        public static DateTime ToMinDate(this DateTime Date)
        {

            return new DateTime(Date.Year, Date.Month, Date.Day, 00, 00, 00);
        }
    //    public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    //(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    //    {
    //        HashSet<TKey> seenKeys = new HashSet<TKey>();
    //        foreach (TSource element in source)
    //        {
    //            if (seenKeys.Add(keySelector(element)))
    //            {
    //                yield return element;
    //            }
    //        }
    //    }
    }
}

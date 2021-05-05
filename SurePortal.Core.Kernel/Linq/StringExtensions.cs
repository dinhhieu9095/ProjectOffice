using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace DaiPhatDat.Core.Kernel.Linq
{
    public static class StringExtensions
    {
        public static string FormatByName(this string format, IDictionary<string, object> values)
        {
            return format.FormatByName(null, values);
        }

        public static string FormatByName(
            this string format,
            IFormatProvider provider,
            IDictionary<string, object> values)
        {
            return format.FormatByName(provider, key =>
            {
                if (values.ContainsKey(key))
                    return values[key];
                return (object)null;
            });
        }

        public static string FormatByName(
            this string format,
            IFormatProvider provider,
            Func<string, object> valueProvider)
        {
            var pattern = "(?<=\\{)(?<key>\\w.*?)(?=\\}|,|:)";
            var empty = string.Empty;
            var args = new List<string>();
            var valuesInOrder = new List<object>();
            var format1 = Regex.Replace(Regex.Replace(format, pattern, m =>
            {
                var num = -1;
                var str = m.Groups["key"].Value;
                var obj = valueProvider(str);
                if (obj != null)
                {
                    if (!args.Contains(str))
                    {
                        args.Add(str);
                        valuesInOrder.Add(obj);
                    }

                    num = args.IndexOf(str);
                }

                return num.ToString();
            }, RegexOptions.IgnoreCase), "{-1(\\:\\w?\\})", m => string.Empty, RegexOptions.IgnoreCase);
            if (format1.Contains("{-1}"))
                format1 = format1.Replace("{-1}", string.Empty);
            return string.Format(provider, format1, valuesInOrder.ToArray());
        }

        public static void ParseFormat(
            this string value,
            bool raiseException,
            Dictionary<string, PropertyDescriptor> pdc,
            out string result,
            out PropertyDescriptor[] pds)
        {
            var propertyDescriptorList = new List<PropertyDescriptor>();
            var startIndex1 = value.IndexOf("{");
            var stringBuilder = new StringBuilder();
            if (startIndex1 == -1)
                stringBuilder.Append(value);
            else
                stringBuilder.Append(value.Substring(0, startIndex1 + 1));
            while (startIndex1 != -1)
            {
                var startIndex2 = value.IndexOf("}", startIndex1);
                if (startIndex1 != -1 && startIndex2 != -1 && startIndex2 > startIndex1)
                {
                    var startIndex3 = value.IndexOfAny(new char[2]
                    {
                        '}',
                        ':'
                    }, startIndex1);
                    var key = value.Substring(startIndex1 + 1, startIndex3 - startIndex1 - 1);
                    var propertyDescriptor = pdc.ContainsKey(key) ? pdc[key] : null;
                    if (propertyDescriptor != null)
                    {
                        stringBuilder.Append(propertyDescriptorList.Count.ToString());
                        propertyDescriptorList.Add(propertyDescriptor);
                    }
                    else
                    {
                        stringBuilder.Append(key);
                    }

                    startIndex1 = value.IndexOf("{", startIndex2);
                    if (startIndex1 == -1)
                        stringBuilder.Append(value.Substring(startIndex3));
                    else
                        stringBuilder.Append(value.Substring(startIndex3, startIndex1 - startIndex3 + 1));
                }
                else
                {
                    if (raiseException)
                        throw new FormatException("No closing char found: " + stringBuilder);
                    result = string.Empty;
                    pds = new PropertyDescriptor[0];
                    return;
                }
            }

            result = stringBuilder.ToString();
            pds = propertyDescriptorList.ToArray();
        }
    }
}
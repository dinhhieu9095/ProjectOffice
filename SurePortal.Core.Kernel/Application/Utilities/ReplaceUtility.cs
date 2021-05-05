using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DaiPhatDat.Core.Kernel.Application.Utilities
{
    public class ReplaceUtility
    {
        public static string ReplaceTextProps(string source, object item)
        {
            string dest = source;
            try
            {
                if (!string.IsNullOrEmpty(source))
                {
                    List<string> replaceProps = CustomSplit(source, "{%", "%}");
                    if (replaceProps != null && replaceProps.Count > 0)
                    {
                        for (int i = 0; i < replaceProps.Count; i++)
                        {
                            string propName = replaceProps[i];
                            string propText = string.Empty;
                            if (propName.Contains(":"))
                            {
                                string name = propName.Substring(0, propName.IndexOf(":"));
                                string format = propName.Substring(propName.IndexOf(":"));
                                object propValue = GetPropValue(item, name);
                                if (propValue != null)
                                {
                                    var formatText = "{0" + format + "}";
                                    propText = string.Format(formatText, propValue);
                                }
                            }
                            else
                            {
                                object propValue = GetPropValue(item, propName);
                                if (propValue != null)
                                {
                                    propText = propValue.ToString();
                                }
                            }
                            dest = dest.Replace("{%" + propName + "%}", propText);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return dest;
        }
        public static object GetPropValue(object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static List<string> CustomSplit(string input, string start, string end)
        {
            List<string> values = new List<string>();
            try
            {
                if (input.Contains(start) && input.Contains(end))
                {
                    string[] result = Regex
                        .Matches(input, @"\" + start + @".*?\" + end)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToArray();
                    foreach (var item in result)
                    {
                        values.Add(item.Replace(start.ToString(), "").Replace(end.ToString(), ""));
                    }
                }
            }
            catch (Exception)
            {
            }
            return values;
        }
    }
}

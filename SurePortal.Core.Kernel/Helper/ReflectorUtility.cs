using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SurePortal.Core.Kernel.Helper
{
    public class ReflectorUtility
    {
        public static object FollowPropertyPath(object value, string path)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (path == null) throw new ArgumentNullException("path");

            Type currentType = value.GetType();

            object obj = value;
            foreach (string propertyName in path.Split('.'))
            {
                if (currentType != null)
                {
                    PropertyInfo property = null;
                    int brackStart = propertyName.IndexOf("[");
                    int brackEnd = propertyName.IndexOf("]");

                    property = currentType.GetProperty(brackStart > 0 ? propertyName.Substring(0, brackStart) : propertyName);
                    if (property != null)
                    {
                        obj = property.GetValue(obj, null);

                        if (brackStart > 0)
                        {
                            string index = propertyName.Substring(brackStart + 1, brackEnd - brackStart - 1);
                            foreach (Type iType in obj.GetType().GetInterfaces())
                            {
                                if (iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                                {
                                    obj = typeof(ReflectorUtility).GetMethod("GetDictionaryElement")
                                                         .MakeGenericMethod(iType.GetGenericArguments())
                                                         .Invoke(null, new object[] { obj, index });
                                    break;
                                }
                                if (iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IList<>))
                                {
                                    obj = typeof(ReflectorUtility).GetMethod("GetListElement")
                                        .MakeGenericMethod(iType.GetGenericArguments())
                                        .Invoke(null, new object[] { obj, index });
                                    break;
                                }
                            }
                        }

                        currentType = obj != null ? obj.GetType() : null; //property.PropertyType;
                    }
                    else return null;
                }
                else return null;
            }
            return obj;
        }
        public static TValue GetDictionaryElement<TKey, TValue>(IDictionary<TKey, TValue> dict, object index)
        {
            TKey key = (TKey)Convert.ChangeType(index, typeof(TKey), null);
            return dict[key];
        }
        public static T GetListElement<T>(IList<T> list, object index)
        {
            int m_Index = Convert.ToInt32(index);
            T m_T = list.Count > m_Index ? list[m_Index] : default(T);

            return m_T;
        }

        /// <summary>
        /// Lấy danh sách tất cả thuộc tính của object
        /// </summary>
        /// <param name="obj">đối tượng cần lấy danh sách thuộc tính </param>
        /// <returns>
        /// Danh sách các thuộc tính PropertyInfo[] 
        /// </returns>
        public static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

    }
}

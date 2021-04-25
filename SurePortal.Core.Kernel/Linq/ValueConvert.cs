using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SurePortal.Core.Kernel.Linq
{
    /// <summary>
    ///     <see cref="T:SurePortal.Core.Kernel.Linq.ValueConvert" /> provides conversion routines for values
    ///     to convert them to another type and routines for formatting values.
    /// </summary>
    internal class ValueConvert
    {
        private static readonly Hashtable cachedDefaultValues = new Hashtable();

        private ValueConvert()
        {
        }

        /// <summary>
        ///     Indicates whether
        ///     <see
        ///         cref="M:SurePortal.Core.Kernel.Linq.ValueConvert.FormatValue(System.Object,System.Type,System.String,System.Globalization.CultureInfo,System.Globalization.NumberFormatInfo)" />
        ///     should trim whitespace characters from
        ///     the end of the formatted text.
        /// </summary>
        public static bool AllowFormatValueTrimEnd { get; set; }

        /// <overload>
        ///     Converts value from one type to another using an optional <see cref="T:System.IFormatProvider" />.
        /// </overload>
        /// <summary>
        ///     Converts value from one type to another using an optional <see cref="T:System.IFormatProvider" />.
        /// </summary>
        /// <param name="value">The original value.</param>
        /// <param name="type">The target type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value.</param>
        /// <returns>The new value in the target type.</returns>
        public static object ChangeType(object value, Type type, IFormatProvider provider)
        {
            return ChangeType(value, type, provider, false);
        }

        /// <summary>
        ///     Converts value from one type to another using an optional <see cref="T:System.IFormatProvider" />.
        /// </summary>
        /// <param name="value">The original value.</param>
        /// <param name="type">The target type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value.</param>
        /// <param name="returnDbNUllIfNotValid">
        ///     Indicates whether exceptions should be avoided or catched and return value should be DBNull if
        ///     it cannot be converted to the target type.
        /// </param>
        /// <returns>The new value in the target type.</returns>
        public static object ChangeType(
            object value,
            Type type,
            IFormatProvider provider,
            bool returnDbNUllIfNotValid)
        {
            return ChangeType(value, type, provider, "", returnDbNUllIfNotValid);
        }

        /// <summary>Tries the parse.</summary>
        /// <param name="s">The string value.</param>
        /// <param name="type">The underline type.</param>
        /// <returns></returns>
        public static bool TryParse(string s, Type type)
        {
            var bindingAttr = BindingFlags.Static | BindingFlags.Public;
            var typeArray = new Type[2]
            {
                typeof(string),
                type.MakeByRefType()
            };
            var method = type.GetMethod(nameof(TryParse), bindingAttr);
            if (!(method != null))
                return true;
            var parameters = new object[2]
            {
                s,
                null
            };
            return (bool)method.Invoke(null, parameters);
        }

        /// <summary>
        ///     Converts value from one type to another using an optional <see cref="T:System.IFormatProvider" />.
        /// </summary>
        /// <param name="value">The original value.</param>
        /// <param name="type">The target type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value.</param>
        /// <param name="format">Format string.</param>
        /// <param name="returnDbNUllIfNotValid">
        ///     Indicates whether exceptions should be avoided or catched and return value should be DBNull if
        ///     it cannot be converted to the target type.
        /// </param>
        /// <returns>The new value in the target type.</returns>
        public static object ChangeType(
            object value,
            Type type,
            IFormatProvider provider,
            string format,
            bool returnDbNUllIfNotValid)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                value = ChangeType(value, underlyingType, provider, true);
                return NullableHelperInternal.FixDbNUllasNull(value, type);
            }

            if (value != null && !type.IsAssignableFrom(value.GetType()))
                try
                {
                    if (value is string)
                    {
                        value = format == null || format.Length <= 0
                            ? Parse((string)value, type, provider, "", returnDbNUllIfNotValid)
                            : Parse((string)value, type, provider, format, returnDbNUllIfNotValid);
                    }
                    else if (!(value is DBNull))
                    {
                        if (type.GetTypeInfo().IsEnum)
                        {
                            value = Convert.ChangeType(value, typeof(int), provider);
                            value = Enum.ToObject(type, (int)value);
                        }
                        else
                        {
                            value = !(type == typeof(string)) || value is IConvertible
                                ?
                                NullableHelperInternal.ChangeType(value, type, provider)
                                : value != null
                                    ? value.ToString()
                                    : "";
                        }
                    }
                }
                catch
                {
                    if (returnDbNUllIfNotValid)
                        return DBNull.Value;
                    throw;
                }

            if ((value == null || value is DBNull) && type == typeof(string))
                return "";
            return value;
        }

        /// <summary>
        ///     Overloaded. Parses the given text using the resultTypes "Parse" method or using a type converter.
        /// </summary>
        /// <param name="s">The text to parse.</param>
        /// <param name="resultType">The requested result type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value. Can be NULL.</param>
        /// <returns>The new value in the target type.</returns>
        private static object Parse(string s, Type resultType, IFormatProvider provider)
        {
            return Parse(s, resultType, provider, "");
        }

        /// <summary>
        ///     Parses the given text using the resultTypes "Parse" method or using a type converter.
        /// </summary>
        /// <param name="s">The text to parse.</param>
        /// <param name="resultType">The requested result type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value. Can be NULL.</param>
        /// <param name="format">
        ///     A format string used in a <see cref="M:System.Object.ToString" /> call. Right now
        ///     format is only interpreted to enable roundtripping for formatted dates.
        /// </param>
        /// <returns>The new value in the target type.</returns>
        public static object Parse(string s, Type resultType, IFormatProvider provider, string format)
        {
            return Parse(s, resultType, provider, format, false);
        }

        /// <summary>
        ///     Parse the given text using the resultTypes "Parse" method or using a type converter.
        /// </summary>
        /// <param name="s">The text to parse.</param>
        /// <param name="resultType">The requested result type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value. Can be NULL.</param>
        /// <param name="format">
        ///     A format string used in a <see cref="M:System.Object.ToString" /> call. Right now
        ///     format is only interpreted to enable roundtripping for formatted dates.
        /// </param>
        /// <param name="returnDbNUllIfNotValid">
        ///     Indicates whether DbNull should be returned if value cannot be parsed. Otherwise
        ///     an exception is thrown.
        /// </param>
        /// <returns>The new value in the target type.</returns>
        public static object Parse(
            string s,
            Type resultType,
            IFormatProvider provider,
            string format,
            bool returnDbNUllIfNotValid)
        {
            return NullableHelperInternal.FixDbNUllasNull(
                _Parse(s, resultType, provider, format, returnDbNUllIfNotValid), resultType);
        }

        /// <summary>
        ///     Parse the given text using the resultTypes "Parse" method or using a type converter.
        /// </summary>
        /// <param name="s">The text to parse.</param>
        /// <param name="resultType">The requested result type.</param>
        /// <param name="provider">A <see cref="T:System.IFormatProvider" /> used to format or parse the value. Can be NULL.</param>
        /// <param name="formats">
        ///     A string array holding permissible formats used in a <see cref="M:System.Object.ToString" /> call. Right now
        ///     formats is only interpreted to enable roundtripping for formatted dates.
        /// </param>
        /// <param name="returnDbNUllIfNotValid">
        ///     Indicates whether DbNull should be returned if value cannot be parsed. Otherwise
        ///     an exception is thrown.
        /// </param>
        /// <returns>The new value in the target type.</returns>
        public static object Parse(
            string s,
            Type resultType,
            IFormatProvider provider,
            string[] formats,
            bool returnDbNUllIfNotValid)
        {
            return NullableHelperInternal.FixDbNUllasNull(
                _Parse(s, resultType, provider, "", formats, returnDbNUllIfNotValid), resultType);
        }

        private static object _Parse(
            string s,
            Type resultType,
            IFormatProvider provider,
            string format,
            bool returnDbNUllIfNotValid)
        {
            return _Parse(s, resultType, provider, format, null, returnDbNUllIfNotValid);
        }

        private static object _Parse(
            string s,
            Type resultType,
            IFormatProvider provider,
            string format,
            string[] formats,
            bool returnDbNUllIfNotValid)
        {
            if (resultType == null)
                return s;
            try
            {
                if (typeof(double).IsAssignableFrom(resultType))
                {
                    if (IsEmpty(s))
                        return DBNull.Value;
                    double result;
                    if (double.TryParse(s, NumberStyles.Any, provider, out result))
                        return Convert.ChangeType(result, resultType, provider);
                    if (returnDbNUllIfNotValid && (resultType == typeof(double) || resultType == typeof(float)))
                        return DBNull.Value;
                }
                else if (typeof(decimal).IsAssignableFrom(resultType))
                {
                    if (IsEmpty(s))
                        return DBNull.Value;
                    decimal result;
                    if (decimal.TryParse(s, NumberStyles.Any, provider, out result))
                        return Convert.ChangeType(result, resultType, provider);
                }
                else
                {
                    if (typeof(DateTime).IsAssignableFrom(resultType))
                    {
                        if (IsEmpty(s))
                            return DBNull.Value;
                        if (formats == null || formats.GetLength(0) == 0 && format.Length > 0)
                            formats = new string[7]
                            {
                                format,
                                "G",
                                "g",
                                "f",
                                "F",
                                "d",
                                "D"
                            };
                        DateTime result1;
                        if (formats != null && formats.GetLength(0) > 0 && DateTime.TryParseExact(s, formats, provider,
                                DateTimeStyles.AllowWhiteSpaces, out result1))
                            return result1;
                        DateTime result2;
                        DateTime.TryParse(s, provider, DateTimeStyles.AllowWhiteSpaces, out result2);
                        return result2;
                    }

                    if (typeof(TimeSpan).IsAssignableFrom(resultType))
                    {
                        if (IsEmpty(s))
                            return DBNull.Value;
                        var flag = false;
                        TimeSpan result;
                        if (TimeSpan.TryParse(s, out result))
                            flag = true;
                        if (flag)
                            return result;
                    }
                    else if (typeof(bool).IsAssignableFrom(resultType))
                    {
                        if (IsEmpty(s))
                            return DBNull.Value;
                        if (s == "1" || s.ToUpper() == bool.TrueString.ToUpper())
                            return true;
                        if (s == "0" || s.ToUpper() == bool.FalseString.ToUpper())
                            return false;
                    }
                    else if (typeof(long).IsAssignableFrom(resultType))
                    {
                        if (IsEmpty(s))
                            return DBNull.Value;
                        long result;
                        if (long.TryParse(s, NumberStyles.Any, provider, out result))
                            return Convert.ChangeType(result, resultType, provider);
                        if (returnDbNUllIfNotValid && resultType.GetTypeInfo().IsPrimitive &&
                            !resultType.GetTypeInfo().IsEnum)
                            return DBNull.Value;
                    }
                    else if (typeof(ulong).IsAssignableFrom(resultType))
                    {
                        if (IsEmpty(s))
                            return DBNull.Value;
                        ulong result;
                        if (ulong.TryParse(s, NumberStyles.Any, provider, out result))
                            return Convert.ChangeType(result, resultType, provider);
                        if (returnDbNUllIfNotValid && resultType.GetTypeInfo().IsPrimitive &&
                            !resultType.GetTypeInfo().IsEnum)
                            return DBNull.Value;
                    }
                    else if (typeof(int).IsAssignableFrom(resultType) || typeof(short).IsAssignableFrom(resultType) ||
                             typeof(float).IsAssignableFrom(resultType) || typeof(uint).IsAssignableFrom(resultType) ||
                             typeof(ushort).IsAssignableFrom(resultType) || typeof(byte).IsAssignableFrom(resultType))
                    {
                        if (IsEmpty(s))
                            return DBNull.Value;
                        double result;
                        if (double.TryParse(s, NumberStyles.Any, provider, out result))
                            return Convert.ChangeType(result, resultType, provider);
                        if (returnDbNUllIfNotValid && resultType.GetTypeInfo().IsPrimitive &&
                            !resultType.GetTypeInfo().IsEnum)
                            return DBNull.Value;
                    }
                    else if (resultType == typeof(Type))
                    {
                        return Type.GetType(s);
                    }
                }

                var converter = TypeDescriptor.GetConverter(resultType);
                if (converter is NullableConverter)
                {
                    var underlyingType = NullableHelperInternal.GetUnderlyingType(resultType);
                    if (underlyingType != null)
                        return _Parse(s, underlyingType, provider, format, formats, returnDbNUllIfNotValid);
                }

                if (converter != null && converter.CanConvertFrom(typeof(string)) && s != null && s.Length > 0)
                    return !(provider is CultureInfo)
                        ? converter.ConvertFrom(s)
                        : converter.ConvertFrom(null, (CultureInfo)provider, s);
            }
            catch
            {
                if (returnDbNUllIfNotValid)
                    return DBNull.Value;
                throw;
            }

            return DBNull.Value;
        }

        /// <summary>
        ///     Generates display text using the specified format, culture info and number format.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="valueType">
        ///     The value type on which formatting is based. The original value will first be converted to this
        ///     type.
        /// </param>
        /// <param name="format">The format like in ToString(string format).</param>
        /// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> for formatting the value.</param>
        /// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> for formatting the value.</param>
        /// <returns>The string with the formatted text for the value.</returns>
        public static string FormatValue(
            object value,
            Type valueType,
            string format,
            CultureInfo ci,
            NumberFormatInfo nfi)
        {
            string str;
            try
            {
                if (value is string)
                    return (string)value;
                if (value is byte[])
                    return "";
                object obj;
                if (value == null || valueType == null || value.GetType() == valueType)
                    obj = value;
                else
                    try
                    {
                        obj = ChangeType(value, valueType, ci, true);
                    }
                    catch (Exception ex)
                    {
                        obj = value;
                        if (!(ex is FormatException) && !(ex.InnerException is FormatException))
                            throw;
                    }

                if (obj == null || obj is DBNull)
                {
                    str = string.Empty;
                }
                else if (obj is IFormattable)
                {
                    var formattable = (IFormattable)obj;
                    var formatProvider = (IFormatProvider)null;
                    if (nfi != null && !(obj is DateTime))
                        formatProvider = nfi;
                    else if (ci != null)
                        formatProvider = obj is DateTime ? ci.DateTimeFormat : (IFormatProvider)ci.NumberFormat;
                    str = format.Length <= 0 && nfi == null
                        ? formattable.ToString()
                        : formattable.ToString(format, formatProvider);
                }
                else
                {
                    var converter = TypeDescriptor.GetConverter(obj.GetType());
                    str = !converter.CanConvertTo(typeof(string))
                        ? !(obj is IConvertible) ? obj.ToString() : Convert.ToString(obj, ci)
                        : (string)converter.ConvertTo(null, ci, obj, typeof(string));
                }
            }
            catch
            {
                var empty = string.Empty;
                throw;
            }

            if (str == null)
                str = string.Empty;
            if (AllowFormatValueTrimEnd)
                str = str.TrimEnd();
            return str;
        }

        /// <summary>Returns a representative value for any given type.</summary>
        /// <param name="type">The <see cref="T:System.Type" />.</param>
        /// <returns>A value with the specified type.</returns>
        public static object GetDefaultValue(Type type)
        {
            if (type == null)
                return "0";
            lock (cachedDefaultValues)
            {
                object obj;
                if (cachedDefaultValues.Contains(type))
                {
                    obj = cachedDefaultValues[type];
                }
                else
                {
                    switch (type.FullName)
                    {
                        case "System.Boolean":
                            obj = true;
                            break;

                        case "System.Byte":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.SByte":
                        case "System.UInt16":
                        case "System.UInt32":
                        case "System.UInt64":
                            obj = 123;
                            break;

                        case "System.Char":
                            obj = 'A';
                            break;

                        case "System.DBNull":
                            obj = DBNull.Value;
                            break;

                        case "System.DateTime":
                            obj = DateTime.Now;
                            break;

                        case "System.Decimal":
                        case "System.Double":
                        case "System.Single":
                            obj = 123.4567;
                            break;

                        case "System.String":
                            obj = string.Empty;
                            break;

                        default:
                            obj = "";
                            break;
                    }

                    cachedDefaultValues[type] = obj;
                }

                return obj;
            }
        }

        /// <summary>
        ///     Overloaded. Parses the given string including type information. String can be in format %lt;type&gt; 'value'
        /// </summary>
        /// <param name="valueAsString"></param>
        /// <param name="retVal"></param>
        /// <returns></returns>
        private static bool ParseValueWithTypeInformation(string valueAsString, out object retVal)
        {
            return ParseValueWithTypeInformation(valueAsString, out retVal);
        }

        /// <summary>
        ///     Parses the given string including type information. String can be in format %lt;type&gt; 'value'
        /// </summary>
        /// <param name="valueAsString"></param>
        /// <param name="retVal"></param>
        /// <param name="allowConvertFromBase64">
        ///     Indicates whether TypeConverter should be checked whether the type to be
        ///     parsed supports conversion to/from byte array (e.g. an Image)
        /// </param>
        /// <returns></returns>
        public static bool ParseValueWithTypeInformation(
            string valueAsString,
            out object retVal,
            bool allowConvertFromBase64)
        {
            retVal = null;
            if (valueAsString.StartsWith("'") && valueAsString.EndsWith("'"))
            {
                retVal = valueAsString.Substring(1, valueAsString.Length - 2);
                return true;
            }

            if (valueAsString.StartsWith("<"))
            {
                var num = valueAsString.IndexOf(">");
                if (num > 1)
                {
                    var typeName = valueAsString.Substring(1, num - 1);
                    if (typeName == "null")
                    {
                        retVal = null;
                        return true;
                    }

                    if (typeName == "System.DBNull")
                    {
                        retVal = DBNull.Value;
                        return true;
                    }

                    valueAsString = valueAsString.Substring(num + 1).Trim();
                    if (valueAsString.StartsWith("'") && valueAsString.EndsWith("'"))
                    {
                        valueAsString = valueAsString.Substring(1, valueAsString.Length - 2);
                        var type = GetType(typeName);
                        if (type != null)
                        {
                            var flag = false;
                            if (allowConvertFromBase64)
                                flag = TryConvertFromBase64String(type, valueAsString, out retVal);
                            if (!flag)
                                retVal = Parse(valueAsString, type, CultureInfo.InvariantCulture, "");
                            return true;
                        }
                    }
                }
            }

            retVal = valueAsString;
            return false;
        }

        /// <summary>
        ///     Indicates whether the TypeConverter associated with the type supports conversion to/from a byte array (e.g. an
        ///     Image).
        ///     If that is the case the string is converted to a byte array from a base64 string.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="valueAsString"></param>
        /// <param name="retVal"></param>
        /// <returns></returns>
        public static bool TryConvertFromBase64String(
            Type type,
            string valueAsString,
            out object retVal)
        {
            var flag = false;
            retVal = null;
            var converter = TypeDescriptor.GetConverter(type);
            if (converter != null)
            {
                if (converter.CanConvertFrom(typeof(byte[])))
                {
                    var numArray = Convert.FromBase64String(valueAsString);
                    retVal = converter.ConvertFrom(numArray);
                    flag = true;
                }
                else if (converter.CanConvertFrom(typeof(MemoryStream)))
                {
                    var memoryStream = new MemoryStream(Convert.FromBase64String(valueAsString));
                    retVal = converter.ConvertFrom(memoryStream);
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        ///     Overloaded. Formats the given value as string including type information. String will be in format %lt;type&gt;
        ///     'value'
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string FormatValueWithTypeInformation(object value)
        {
            return FormatValueWithTypeInformation(value, false);
        }

        /// <summary>
        ///     Formats the given value as string including type information. String will be in format %lt;type&gt; 'value'
        /// </summary>
        /// <param name="value"></param>
        /// <param name="allowConvertToBase64">
        ///     Indicates whether TypeConverter should be checked whether the type to be
        ///     parsed supports conversion to/from byte array (e.g. an Image)
        /// </param>
        /// <returns></returns>
        public static string FormatValueWithTypeInformation(object value, bool allowConvertToBase64)
        {
            if (value is string)
                return "'" + (string)value + "'";
            if (value is DBNull)
                return "<System.DBNull>";
            if (value == null)
                return "<null>";
            var str = null ?? FormatValue(value, typeof(string), "", CultureInfo.InvariantCulture, null);
            return "<" + GetTypeName(value.GetType()) + "> '" + str + "'";
        }

        /// <summary>
        ///     Returns the type name. If type is not in mscorlib, the assembly name is appended.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeName(Type type)
        {
            return type.FullName;
        }

        /// <summary>
        ///     Returns the type from the specified name. If an assembly name is appended the list of currently loaded
        ///     assemblies in the current AppDomain are checked.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetType(string typeName)
        {
            return Type.GetType(typeName);
        }

        /// <summary>Indicates whether string is null or empty.</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(string str)
        {
            return str == null || str.Length == 0;
        }
    }
}
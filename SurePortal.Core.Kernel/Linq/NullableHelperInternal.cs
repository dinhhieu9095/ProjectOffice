using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace SurePortal.Core.Kernel.Linq
{
    /// <summary>
    ///     A framework independent utility class for the new Nullable type in .NET Framework 2.0
    /// </summary>
    internal class NullableHelperInternal
    {
        /// <summary>
        ///     Indicates whether the specified PropertyDescriptor has nested properties.
        /// </summary>
        /// <param name="pd">The PropertyDescriptor to be checked.</param>
        /// <returns>True if nested properties are found; False otherwise.</returns>
        internal static bool IsComplexType(PropertyInfo pd)
        {
            return IsComplexType(pd.PropertyType);
        }

        /// <summary>
        ///     Indicates whether the specified Type has nested properties.
        /// </summary>
        /// <param name="t">The Type to be checked.</param>
        /// <returns>True if nested properties are found; False otherwise.</returns>
        public static bool IsComplexType(Type t)
        {
            var underlyingType = GetUnderlyingType(t);
            if (underlyingType != null)
                t = underlyingType;
            return t != typeof(object) && t != typeof(decimal) && t != typeof(DateTime) && t != typeof(Type) &&
                   t != typeof(string) && t != typeof(Guid) && t.GetTypeInfo().BaseType != typeof(Enum) &&
                   !t.GetTypeInfo().IsPrimitive;
        }

        public static bool IsIEnumerableType(PropertyInfo pd)
        {
            return IsComplexType(pd.PropertyType) && !typeof(byte[]).IsAssignableFrom(pd.PropertyType) &&
                   pd.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(pd.PropertyType) &&
                   (!pd.PropertyType.IsArray || !pd.PropertyType.GetElementType().GetTypeInfo().IsPrimitive);
        }

        /// <summary>
        ///     Use this method instead of Convert.ChangeType. Makes Convert.ChangeType work with Nullable types.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                if (value is string && underlyingType != typeof(string) && ValueConvert.IsEmpty((string)value))
                    return null;
                value = ChangeType(value, underlyingType);
                if (value is DBNull)
                    return null;
                return value;
            }

            if (!type.GetTypeInfo().IsInterface)
                return TypeConverterHelper.ChangeType(value, type);
            return value;
        }

        /// <summary>
        ///     Use this method instead of Convert.ChangeType. Makes Convert.ChangeType work with Nullable types.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type type, IFormatProvider provider)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (!(underlyingType != null))
                return TypeConverterHelper.ChangeType(value, type, provider);
            if (value is string && underlyingType != typeof(string) && ValueConvert.IsEmpty((string)value))
                return null;
            value = ChangeType(value, underlyingType, provider);
            if (value is DBNull)
                return null;
            return value;
        }

        public static Type GetNullableType(Type type)
        {
            if (type == null)
                return null;
            if (IsNullableType(type))
                return type;
            var type1 = Nullable.GetUnderlyingType(type);
            if ((object)type1 == null)
                type1 = type;
            if (!type1.GetTypeInfo().IsValueType)
                return type;
            return typeof(Nullable<>).MakeGenericType(type);
        }

        public static bool IsNullableType(Type nullableType)
        {
            if (nullableType == null)
                throw new ArgumentNullException(nameof(nullableType));
            var flag = false;
            if (nullableType.GetTypeInfo().IsGenericType && !nullableType.GetTypeInfo().IsGenericTypeDefinition &&
                nullableType.GetGenericTypeDefinition() == typeof(Nullable<>))
                flag = true;
            return flag;
        }

        /// <summary>
        ///     Returns null if value is DBNull and specified type is a Nullable type. Otherwise the value is returned unchanged.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object FixDbNUllasNull(object value, Type type)
        {
            if (type == null || (!(Nullable.GetUnderlyingType(type) != null) || !(value is DBNull)) &&
                (type.GetTypeInfo().IsValueType || !(value is DBNull)))
                return value;
            return null;
        }

        /// <summary>
        ///     Returns the underlying type of a Nullable type. For .NET 1.0 and 1.1 this method will always return null.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetUnderlyingType(Type type)
        {
            return type == (Type)null ? null : Nullable.GetUnderlyingType(type);
        }

        /// <exclude />
        private class TypeConverterHelper
        {
            public static object ChangeType(object value, Type type)
            {
                return ChangeType(value, type, null);
            }

            public static object ChangeType(object value, Type type, IFormatProvider provider)
            {
                if (value == null)
                    return null;
                var converter = TypeDescriptor.GetConverter(value.GetType());
                if (converter != null && converter.CanConvertTo(type))
                    return converter.ConvertTo(value, type);
                if (value is DBNull)
                    return DBNull.Value;
                try
                {
                    return Convert.ChangeType(value, type, provider);
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
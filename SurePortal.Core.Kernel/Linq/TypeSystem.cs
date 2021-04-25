using System;
using System.Collections.Generic;
using System.Reflection;

namespace SurePortal.Core.Kernel.Linq
{
    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            var ienumerable = FindIEnumerable(seqType);
            if (ienumerable == null)
                return seqType;
            return ienumerable.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.GetTypeInfo().IsGenericType)
                foreach (var genericArgument in seqType.GetGenericArguments())
                {
                    var type = typeof(IEnumerable<>).MakeGenericType(genericArgument);
                    if (type.IsAssignableFrom(seqType))
                        return type;
                }

            var interfaces = seqType.GetInterfaces();
            if (interfaces != null && (uint)interfaces.Length > 0U)
                foreach (var seqType1 in interfaces)
                {
                    var ienumerable = FindIEnumerable(seqType1);
                    if (ienumerable != null)
                        return ienumerable;
                }

            if (seqType.GetTypeInfo().BaseType != null && seqType.GetTypeInfo().BaseType != typeof(object))
                return FindIEnumerable(seqType.GetTypeInfo().BaseType);
            return null;
        }
    }
}
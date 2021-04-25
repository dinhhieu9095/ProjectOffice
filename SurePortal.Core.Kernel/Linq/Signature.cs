using System;
using System.Collections.Generic;
using System.Linq;

namespace SurePortal.Core.Kernel.Linq
{
    internal class Signature : IEquatable<Signature>
    {
        public int hashCode;
        public DynamicProperty[] properties;

        public Signature(IEnumerable<DynamicProperty> properties)
        {
            this.properties = properties.ToArray();
            hashCode = 0;
            foreach (var property in properties)
                hashCode ^= property.Name.GetHashCode() ^ property.Type.GetHashCode();
        }

        public bool Equals(Signature other)
        {
            if (properties.Length != other.properties.Length)
                return false;
            for (var index = 0; index < properties.Length; ++index)
                if (properties[index].Name != other.properties[index].Name ||
                    properties[index].Type != other.properties[index].Type)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Signature && Equals((Signature)obj);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
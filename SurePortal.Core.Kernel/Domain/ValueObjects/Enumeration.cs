using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SurePortal.Core.Kernel.Domain.ValueObjects
{
    public abstract class Enumeration : IComparable
    {
        public int Value { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }


        protected Enumeration(int value, string code, string name)
        {
            Value = value;
            Code = code;
            Name = name;
        }

        public override string ToString() => Code;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }
        public static T FromCode<T>(string code) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(code, "code",
                item => item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            return matchingItem;
        }
        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name",
                item => item.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase));
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object obj) => Value.CompareTo(((Enumeration)obj).Value);
    }
}

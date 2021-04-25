using System.Reflection;
using System.Text;

namespace SurePortal.Core.Kernel.Linq
{
    public abstract class DynamicClass
    {
        public override string ToString()
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            for (var index = 0; index < properties.Length; ++index)
            {
                if (index > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(properties[index].Name);
                stringBuilder.Append("=");
                stringBuilder.Append(properties[index].GetValue(this, null));
            }

            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
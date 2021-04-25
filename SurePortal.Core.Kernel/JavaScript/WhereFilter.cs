using System.Collections.Generic;

namespace SurePortal.Core.Kernel.JavaScript
{
    public class WhereFilter
    {
        public string Field { get; set; }

        public bool IgnoreCase { get; set; }

        public bool IsComplex { get; set; }

        public string Operator { get; set; }

        public string Condition { get; set; }

        public object Value { get; set; }

        public List<WhereFilter> Predicates { get; set; }
    }
}
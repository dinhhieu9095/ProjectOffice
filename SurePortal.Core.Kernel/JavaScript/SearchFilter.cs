using System.Collections.Generic;

namespace SurePortal.Core.Kernel.JavaScript
{
    public class SearchFilter
    {
        public List<string> Fields { get; set; }

        public string Key { get; set; }

        public string Operator { get; set; }
    }
}
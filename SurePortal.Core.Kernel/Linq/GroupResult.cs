using System.Collections;
using System.Collections.Generic;

namespace SurePortal.Core.Kernel.Linq
{
    public class GroupResult
    {
        public object Key { get; set; }

        public int Count { get; set; }

        public IEnumerable Items { get; set; }

        public IEnumerable<GroupResult> SubGroups { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Key, Count);
        }
    }
}
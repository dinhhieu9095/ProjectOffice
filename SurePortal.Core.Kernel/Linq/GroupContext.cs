using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SurePortal.Core.Kernel.Linq
{
    public class GroupContext
    {
        public List<GroupContext> ChildGroups { get; set; }

        public int Count { get; set; }

        public IEnumerable Details { get; set; }

        public object Key
        {
            get
            {
                if (Details != null)
                    return Details.GetType().GetProperties().Where(pi => pi.Name == nameof(Key)).FirstOrDefault()
                        .GetValue(Details, null);
                return null;
            }
        }
    }
}
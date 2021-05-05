using System.Collections.Generic;

namespace DaiPhatDat.Core.Kernel.JavaScript
{
    public class DataManager
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public string AntiForgery { get; set; }

        public bool RequiresCounts { get; set; }

        public string Table { get; set; }

        public List<string> Group { get; set; }

        public List<string> Select { get; set; }

        public List<string> Expand { get; set; }

        public List<Sort> Sorted { get; set; }

        public List<SearchFilter> Search { get; set; }

        public List<WhereFilter> Where { get; set; }

        public List<Aggregate> Aggregates { get; set; }
        public Dictionary<string, string> Params { get; set; }
    }

}
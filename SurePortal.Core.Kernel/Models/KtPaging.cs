
namespace DaiPhatDat.Core.Kernel.Models
{
    public class KtPaging
    {
        public string Field { get; set; }

        public int Page { get; set; }

        public int Pages { get; set; }

        public int Perpage { get; set; }

        public string Sort { get; set; }

        public int Total { get; set; }
    }

    public class KtSort
    {
        public string Sort { get; set; }

        public string Field { get; set; }
    }
}

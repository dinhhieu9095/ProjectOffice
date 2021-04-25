
namespace SurePortal.Core.Kernel.Linq
{
    public class SortColumn
    {
        public SortColumn()
        {
            SortDirection = ListSortDirection.Ascending;
        }

        public string ColumnName { get; set; }

        public ListSortDirection SortDirection { get; set; }
    }
}

namespace DaiPhatDat.Core.Kernel.Linq
{
    public struct SortDescription
    {
        public SortDescription(string propertyName, ListSortDirection direction)
        {
            Direction = direction;
            PropertyName = propertyName;
        }

        public static bool operator !=(SortDescription sd1, SortDescription sd2)
        {
            return false;
        }

        public static bool operator ==(SortDescription sd1, SortDescription sd2)
        {
            return false;
        }

        public ListSortDirection Direction { get; set; }

        public string PropertyName { get; set; }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
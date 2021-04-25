
namespace SurePortal.Core.Kernel.JavaScript.Models
{
    /// <summary>
    ///     Gets or sets a value that indicates whether to define the direction and field to sort the column.
    /// </summary>
    public class SortedColumn
    {
        /// <summary>
        ///     Gets or sets a value that indicates whether to define the field name of the column to be sort.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether to define the direction to sort the column.
        /// </summary>
        public SortOrder Direction { get; set; } = SortOrder.Ascending;
    }
}

namespace SurePortal.Core.Kernel.Linq
{
    /// <summary>
    ///     Specifies the Filter Behaviour for the filter predicates.
    /// </summary>
    public enum FilterBehavior
    {
        /// <summary>Parses only StronglyTyped values.</summary>
        StronglyTyped,

        /// <summary>Parses all values by converting them as string.</summary>
        StringTyped
    }
}
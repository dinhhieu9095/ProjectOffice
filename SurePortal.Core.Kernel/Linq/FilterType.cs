
namespace SurePortal.Core.Kernel.Linq
{
    /// <summary>Specifies the FilterType to be used in LINQ methods.</summary>
    public enum FilterType
    {
        /// <summary>Performs LessThan operation.</summary>
        LessThan,

        /// <summary>Performs LessThan Or Equal operation.</summary>
        LessThanOrEqual,

        /// <summary>Checks Equals on the operands.</summary>
        Equals,

        /// <summary>Checks for Not Equals on the operands.</summary>
        NotEquals,

        /// <summary>Checks for Greater Than or Equal on the operands.</summary>
        GreaterThanOrEqual,

        /// <summary>Checks for Greater Than on the operands.</summary>
        GreaterThan,

        /// <summary>Checks for StartsWith on the string operands.</summary>
        StartsWith,

        /// <summary>Checks for EndsWith on the string operands.</summary>
        EndsWith,

        /// <summary>Checks for Contains on the string operands.</summary>
        Contains,

        /// <summary>Returns invalid type</summary>
        Undefined,

        /// <summary>Checks for Between two date on the operands.</summary>
        Between
    }
}
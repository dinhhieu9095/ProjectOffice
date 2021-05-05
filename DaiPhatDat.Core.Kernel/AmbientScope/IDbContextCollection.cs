using System;
using System.Data.Entity;

namespace DaiPhatDat.Core.Kernel.AmbientScope
{
    /// <inheritdoc />
    /// <summary>
    ///     Maintains a list of lazily-created DbContext instances.
    /// </summary>
    public interface IDbContextCollection : IDisposable
    {
        /// <summary>
        ///     Get or create a DbContext instance of the specified type.
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
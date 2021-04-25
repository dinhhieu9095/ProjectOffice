using System.Data;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public class DbContextReadOnlyScope : IDbContextReadOnlyScope
    {
        private readonly DbContextScope _internalScope;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public DbContextReadOnlyScope(IDbContextFactory dbContextFactory = null)
            : this(DbContextScopeOption.JoinExisting, null, dbContextFactory)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="dbContextFactory"></param>
        public DbContextReadOnlyScope(IsolationLevel isolationLevel
            , IDbContextFactory dbContextFactory = null)
            : this(DbContextScopeOption.ForceCreateNew, isolationLevel, dbContextFactory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="joiningOption"></param>
        /// <param name="isolationLevel"></param>
        /// <param name="dbContextFactory"></param>
        public DbContextReadOnlyScope(DbContextScopeOption joiningOption
            , IsolationLevel? isolationLevel
            , IDbContextFactory dbContextFactory = null)
        {
            _internalScope = new DbContextScope(joiningOption, true, isolationLevel, dbContextFactory);
        }

        public IDbContextCollection DbContexts => _internalScope.DbContexts;

        public void Dispose()
        {
            _internalScope.Dispose();
        }
    }
}
using System;
using System.Data;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public class DbContextScopeFactory : IDbContextScopeFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        /// <summary>
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public DbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="joiningOption"></param>
        /// <returns></returns>
        public IDbContextScope Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextScope(joiningOption
                , false
                , null
                , _dbContextFactory);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="joiningOption"></param>
        /// <returns></returns>
        public IDbContextReadOnlyScope CreateReadOnly(
            DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextReadOnlyScope(joiningOption
                , null
                , _dbContextFactory);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IDbContextScope CreateWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextScope(DbContextScopeOption.ForceCreateNew
                , false
                , isolationLevel
                , _dbContextFactory);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IDbContextReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextReadOnlyScope(DbContextScopeOption.ForceCreateNew
                , isolationLevel
                , _dbContextFactory);
        }

        public IDisposable SuppressAmbientContext()
        {
            return new AmbientContextSuppressor();
        }
    }
}
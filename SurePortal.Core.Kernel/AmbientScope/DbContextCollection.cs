using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.AmbientScope
{
    /// <inheritdoc />
    /// <summary>
    ///     As its name suggests, DbContextCollection maintains a collection of DbContext instances.
    ///     What it does in a nutshell:
    ///     - Lazily instantiates DbContext instances when its Get Of TDbContext () method is called
    ///     (and optionally starts an explicit database transaction).
    ///     - Keeps track of the DbContext instances it created so that it can return the existing
    ///     instance when asked for a DbContext of a specific type.
    ///     - Takes care of committing / rolling back changes and transactions on all the DbContext
    ///     instances it created when its Commit() or Rollback() method is called.
    /// </summary>
    public class DbContextCollection : IDbContextCollection
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IsolationLevel? _isolationLevel;
        private readonly bool _readOnly;
        private readonly Dictionary<DbContext, DbContextTransaction> _transactions;
        private bool _completed;
        private bool _disposed;

        /// <summary>
        /// </summary>
        /// <param name="readOnly"></param>
        /// <param name="isolationLevel"></param>
        /// <param name="dbContextFactory"></param>
        public DbContextCollection(bool readOnly = false
            , IsolationLevel? isolationLevel = null
            , IDbContextFactory dbContextFactory = null)
        {
            _disposed = false;
            _completed = false;

            _transactions = new Dictionary<DbContext, DbContextTransaction>();

            _readOnly = readOnly;

            _isolationLevel = isolationLevel;
            _dbContextFactory = dbContextFactory;

            InitializedDbContexts = new Dictionary<Type, DbContext>();
        }

        internal Dictionary<Type, DbContext> InitializedDbContexts { get; }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));

            var requestedType = typeof(TDbContext);

            if (InitializedDbContexts.ContainsKey(requestedType))
                return InitializedDbContexts[requestedType] as TDbContext;

            // First time we've been asked for this particular DbContext type.
            // Create one, cache it and start its database transaction if needed.
            var dbContext = _dbContextFactory != null
                ? _dbContextFactory.CreateDbContext<TDbContext>()
                : Activator.CreateInstance<TDbContext>();

            InitializedDbContexts.Add(requestedType, dbContext);

            if (_readOnly) dbContext.Configuration.AutoDetectChangesEnabled = false;

            dbContext.Configuration.ProxyCreationEnabled = false;
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ValidateOnSaveEnabled = false;

            //   DbInterception.Add(new FullTextInterceptor());

            if (!_isolationLevel.HasValue) return InitializedDbContexts[requestedType] as TDbContext;

            var transaction = dbContext.Database.BeginTransaction(_isolationLevel.Value);

            _transactions.Add(dbContext, transaction);

            return InitializedDbContexts[requestedType] as TDbContext;
        }

        public void Dispose()
        {
            if (_disposed) return;

            // Do our best here to dispose as much as we can even if we get errors along the way.
            // Now is not the time to throw. Correctly implemented applications will have called
            // either Commit() or Rollback() first and would have got the error there.

            if (!_completed)
                try
                {
                    if (_readOnly)
                        Commit();
                    else
                        Rollback();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }

            foreach (var dbContext in InitializedDbContexts.Values)
                try
                {
                    dbContext.Dispose();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }

            InitializedDbContexts.Clear();

            _disposed = true;
        }

        public int Commit()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));

            if (_completed)
                throw new InvalidOperationException(@"
                    You can't call Commit() or Rollback() more than once on a DbContextCollection.
                    All the changes in the DbContext instances managed by this collection have already been
                    saved or rollback and all database transactions have been completed and closed.
                    If you wish to make more data changes,
                    create a new DbContextCollection and make your changes there.");

            // Best effort. You'll note that we're not actually implementing an atomic commit
            // here. It entirely possible that one DbContext instance will be committed successfully
            // and another will fail. Implementing an atomic commit would require us to wrap
            // all of this in a TransactionScope. The problem with TransactionScope is that
            // the database transaction it creates may be automatically promoted to a
            // distributed transaction if our DbContext instances happen to be using different
            // databases. And that would require the DTC service (Distributed Transaction Coordinator)
            // to be enabled on all of our live and dev servers as well as on all of our dev workstations.
            // Otherwise the whole thing would blow up at runtime.

            // In practice, if our services are implemented following a reasonably DDD approach,
            // a business transaction (i.e. a service method) should only modify entities in a single
            // DbContext. So we should never find ourselves in a situation where two DbContext instances
            // contain uncommitted changes here. We should therefore never be in a situation where the below
            // would result in a partial commit.

            ExceptionDispatchInfo lastError = null;

            var changes = 0;

            foreach (var dbContext in InitializedDbContexts.Values)
                try
                {
                    if (!_readOnly) changes += dbContext.SaveChanges();

                    // If we've started an explicit database transaction, time to commit it now.
                    var transaction = GetValueOrDefault(_transactions, dbContext);

                    if (transaction == null) continue;

                    transaction.Commit();
                    transaction.Dispose();
                }
                catch (Exception exception)
                {
                    lastError = ExceptionDispatchInfo.Capture(exception);
                }

            _transactions.Clear();
            _completed = true;

            lastError?.Throw(); // Re-throw while maintaining the exception's original stack track

            return changes;
        }

        public Task<int> CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        /// <summary>
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task<int> CommitAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null) throw new ArgumentNullException(nameof(cancelToken));

            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));

            if (_completed)
                throw new InvalidOperationException(@"
                    You can't call Commit() or Rollback() more than once on a DbContextCollection.
                    All the changes in the DbContext instances managed by this collection have already been
                    saved or rollback and all database transactions have been completed and closed.
                    If you wish to make more data changes,
                    create a new DbContextCollection and make your changes there.");

            // See comments in the sync version of this method for more details.

            ExceptionDispatchInfo lastError = null;

            var changes = 0;

            foreach (var dbContext in InitializedDbContexts.Values)
                try
                {
                    if (!_readOnly) changes += await dbContext.SaveChangesAsync(cancelToken).ConfigureAwait(false);

                    // If we've started an explicit database transaction, time to commit it now.
                    var transaction = GetValueOrDefault(_transactions, dbContext);

                    if (transaction == null) continue;

                    transaction.Commit();
                    transaction.Dispose();
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }

            _transactions.Clear();
            _completed = true;

            lastError?.Throw(); // Re-throw while maintaining the exception's original stack track

            return changes;
        }

        public void Rollback()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));

            if (_completed)
                throw new InvalidOperationException(@"
                    You can't call Commit() or Rollback() more than once on a DbContextCollection.
                    All the changes in the DbContext instances managed by this collection have already been
                    saved or rollback and all database transactions have been completed and closed.
                    If you wish to make more data changes,
                    create a new DbContextCollection and make your changes there.");

            ExceptionDispatchInfo lastError = null;

            foreach (var dbContext in InitializedDbContexts.Values)
            {
                // There's no need to explicitly rollback changes in a DbContext as
                // DbContext doesn't save any changes until its SaveChanges() method is called.
                // So "rolling back" for a DbContext simply means not calling its SaveChanges()
                // method.

                // But if we've started an explicit database transaction, then we must roll it back.
                var transaction = GetValueOrDefault(_transactions, dbContext);

                if (transaction == null) continue;

                try
                {
                    transaction.Rollback();
                    transaction.Dispose();
                }
                catch (Exception exception)
                {
                    lastError = ExceptionDispatchInfo.Capture(exception);
                }
            }

            _transactions.Clear();
            _completed = true;

            lastError?.Throw(); // Re-throw while maintaining the exception's original stack track
        }

        /// <summary>
        ///     Returns the value associated with the specified key or the default
        ///     value for the TValue  type.
        /// </summary>
        private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }
    }
}
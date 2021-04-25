using AutoMapper;
using AutoMapper.QueryableExtensions;
using RefactorThis.GraphDiff;
using SurePortal.Core.Kernel.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Unity.Attributes;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, new()
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;
        protected Repository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }
        protected DbContext DbContext
            => _ambientDbContextLocator.Get<TDbContext>() ?? throw new InvalidOperationException(
                   $"No ambient DbContext of type {nameof(TDbContext)} found. " + @"
                    This means that this repository method has been called outside of the scope of a DbContextScope.
                    A repository must only be accessed within the scope of a DbContextScope,
                    which takes care of creating the DbContext instances
                    that the repositories need and making them available as ambient contexts.
                    This is what ensures that, for any given DbContext-derived type,
                    the same instance is used throughout the duration of a business transaction.
                    To fix this issue, use IDbContextScopeFactory in your top-level business logic service method
                    to create a DbContextScope that wraps the entire business transaction that your service method implements.
                    Then access this repository within that scope.
                    Refer to the comments in the DbContextScope.cs file for more details.");
        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
        [Dependency] public IMapper Mapper { get; set; }

        #region IQueryable
        public IQueryable<TProject> Query<TProject>() where TProject : class, IMapping
        {
            return DbSet.ProjectTo<TProject>(Mapper.ConfigurationProvider);
        }
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
        private IQueryable<TEntity> Queryable(
            Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            OrderingQuery<TEntity> orderingQuery = null,
            PagingQuery pagingQuery = null)
        {
            return DbSet.Where(predicate).Including(includingQuery).Ordering(orderingQuery).Paging(pagingQuery);
        }

        #endregion

        #region Get
        public TEntity Find(
            Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null)
        {
            return Queryable(predicate, includingQuery).SingleOrDefault();
        }
        public async Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            CancellationToken cancellationToken = default)
        {
            return await Queryable(predicate, includingQuery).SingleOrDefaultAsync(cancellationToken);
        }
        public IReadOnlyList<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            OrderingQuery<TEntity> orderingQuery = null,
            PagingQuery pagingQuery = null)
        {
            return Queryable(predicate, includingQuery, orderingQuery, pagingQuery).ToList();
        }
        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            OrderingQuery<TEntity> orderingQuery = null,
            PagingQuery pagingQuery = null,
            CancellationToken cancellationToken = default)
        {
            return await Queryable(predicate, includingQuery, orderingQuery, pagingQuery)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Add
        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Entry(entity).State = EntityState.Added;
        }
        public void AddRange(List<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        #endregion

        #region Modify
        public void Modify(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Modify(TEntity entity,
            List<Expression<Func<TEntity, object>>> properties)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entry = DbContext.Entry(entity);

            entry.State = EntityState.Modified;

            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
        }
        public void Modify(
            TEntity entity,
            Expression<Func<IUpdateConfiguration<TEntity>, object>> expression,
            bool allowDelete = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UpdateGraph(entity, expression, allowDelete);
        }
        private void UpdateGraph(
            TEntity entity,
            Expression<Func<IUpdateConfiguration<TEntity>, object>> expression,
            bool allowDelete = false)
        {
            // SET STATE TO DETACHED IS VERY IMPORTANT STEP
            // SPECIFIC WHAT IS ACTUALLY PERSISTED IN EXPRESSION PARAMS
            // AVOID SOMETHING WE DO NOT KNOW GET PERSISTED TO THE DATABASE
            DbContext.Entry(entity).State = EntityState.Detached;
            DbContext.UpdateGraph(entity, expression, allowDelete);
        }

        #endregion

        #region Delete
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Entry(entity).State = EntityState.Deleted;
        }
        public void DeleteRange(List<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
        public void Delete(
            TEntity entity,
            Expression<Func<IUpdateConfiguration<TEntity>, object>> expression,
            Func<TEntity, bool> predicate = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UpdateGraph(entity, expression, true);

            if (predicate == null)
            {
                var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
                var memberExpression = Expression.PropertyOrField(parameterExpression, "Id");

                var binaryExpression = Expression.Equal(
                    memberExpression,
                    Expression.Constant(entity.GetType().GetProperty("Id")?.GetValue(entity, null)));

                predicate = Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameterExpression).Compile();
            }

            Delete(DbSet.Local.Single(predicate));
        }

        #endregion

        #region Count
        public int Count()
        {
            return DbSet.Count();
        }
        public async Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default)
        {
            return await Queryable(predicate).CountAsync(cancellationToken);
        }
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.CountAsync(cancellationToken);
        }

        #endregion

        #region SqlQuery

        public virtual IQueryable<T> SqlQuery<T>(string sql) where T : class, new()
        {
            return DbContext.Database.SqlQuery<T>(sql).AsQueryable();
        }
        public async Task<List<object>> SqlQueryAsync(Type elementType, string sql, object[] parameters = null)
        {
            return await DbContext.Database.SqlQuery(elementType, sql,
                parameters != null ? parameters : new object[] { }).ToListAsync();
        }
        #endregion
    }
}
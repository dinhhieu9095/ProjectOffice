using AutoMapper;
using AutoMapper.QueryableExtensions;
using RefactorThis.GraphDiff;
using DaiPhatDat.Core.Kernel.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Unity.Attributes;

namespace DaiPhatDat.Core.Kernel.AmbientScope
{
    #region RepositoryBase
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private DbContext context;
        private DbSet<TEntity> dbSet;

        public RepositoryBase(DbContext context)
        {

            this.context = context as DbContext;
            if (this.context != null)
            {
                this.dbSet = this.context.Set<TEntity>();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public IList<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public IList<TEntity> GetAll(out int total, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> selectFields = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null, int? pageSize = null)
        {
            total = this.Select(filter).Count();
            return this.Select(filter, orderBy, selectFields, includes, pageNumber, pageSize).ToList();
        }

        public IList<TEntity> GetAll(out int total,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            return this.GetAll(out total, filter, orderBy, null, includes, pageNumber, pageSize);
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            return this.Select(filter, orderBy, includes, null, null).ToList();
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.Select(filter, null, null, null, null).ToList();
        }
        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> selectFields = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (selectFields != null)
            {
                foreach (var m_SelectField in selectFields)
                {
                    query.Select(m_SelectField);
                }
            }

            if (pageNumber != null && pageSize != null)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }



        public IList<TResult> GetByStoreProcedure<TResult>(string storeName, params object[] parameters)
        {
            var m_Method = this.context.GetType().GetMethod(storeName);
            var m_StoreResult = m_Method.Invoke(this.context, parameters) as ObjectResult<TResult>;

            return m_StoreResult.ToList();
        }

        public TResult ExecuteStoreProcedure<TResult>(string storeName, params object[] parameters)
        {
            var m_Method = this.context.GetType().GetMethod(storeName);
            return (TResult)m_Method.Invoke(this.context, parameters);
        }



        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return dbSet;
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
        {
            return ((IObjectContextAdapter)context).ObjectContext.ExecuteFunction<TElement>(functionName, parameters);
        }
    }

    #endregion

    #region Repository Context
    public class RepositoryContext<TContext> : IRepositoryContext<TContext> where TContext : DbContext
    {
        #region ctor
        public TContext DbContext { get; }
        public RepositoryContext(TContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Get List
        public virtual IQueryable<T> GetAll<T>() where T : class, new()
        {
            try
            {
                return DbContext.Set<T>();
            }
            catch (Exception ex) { throw ex; }
        }
        public virtual IQueryable<T> GetAll<T>(Expression<Func<T, bool>> selector) where T : class, new()
        {
            try
            {
                return DbContext.Set<T>().Where(selector);
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Get 

        public virtual T GetById<T>(Guid Id, Expression<Func<T, object>> expressionOrder = null) where T : class, new()
        {
            try
            {
                return DbContext.Set<T>().Find(Id);
            }
            catch (Exception ex) { throw ex; }
        }
        public T SelectOne<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            try
            {
                return DbContext.Set<T>().FirstOrDefault(predicate);
            }
            catch (Exception ex) { throw ex; }
        }


        #endregion

        #region Insert
        public virtual T Insert<T>(T entity) where T : class, new()
        {
            try
            {
                DbContext.Set<T>().Add(entity);
                DbContext.SaveChanges();
                return entity;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual List<T> InsertToList<T>(List<T> entity) where T : class, new()
        {
            try
            {
                foreach (var item in entity)
                {
                    DbContext.Set<T>().Add(item);
                }
                DbContext.SaveChanges();
                return entity;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual async Task<T> InsertAsync<T>(T insertObject) where T : class, new()
        {
            try
            {
                DbContext.Set<T>().Add(insertObject);
                await DbContext.SaveChangesAsync();
                return insertObject;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual async Task<List<T>> InsertToListAsync<T>(List<T> entity) where T : class, new()
        {
            try
            {
                foreach (var item in entity)
                {
                    DbContext.Set<T>().Add(item);
                }
                await DbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Update
        public virtual bool Update<T>(T updateEntity) where T : class, new()
        {
            try
            {
                DbContext.Set<T>().Attach(updateEntity);
                DbContext.Entry(updateEntity).State = EntityState.Modified;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool UpdateToList<T>(List<T> entity) where T : class, new()
        {
            bool result = false;
            try
            {
                foreach (var item in entity)
                {
                    DbContext.Set<T>().Attach(item);
                    DbContext.Entry(item).State = EntityState.Modified;
                }
                DbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex) { throw ex; }
            return result;
        }

        public virtual async Task<bool> UpdateAsync<T>(T updateEntity) where T : class, new()
        {
            bool result = false;
            try
            {
                DbContext.Set<T>().Attach(updateEntity);
                DbContext.Entry(updateEntity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex) { }
            return result;
        }

        public virtual async Task<bool> UpdateToListAsync<T>(List<T> entity) where T : class, new()
        {
            bool result = false;
            try
            {
                foreach (var item in entity)
                {
                    DbContext.Set<T>().Attach(item);
                    DbContext.Entry(item).State = EntityState.Modified;
                }
                await DbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex) { throw ex; }
            return result;
        }
        #endregion

        #region Delete
        public virtual bool DeleteById<T>(Guid id) where T : class, new()
        {
            try
            {
                var del = DbContext.Set<T>().Find(id);

                if (del != null)
                {
                    DbContext.Set<T>().Attach(del);
                    DbContext.Entry(del).State = EntityState.Deleted;
                    DbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public virtual async Task<bool> DeleteByIdAsync<T>(Guid id) where T : class, new()
        {
            try
            {
                var del = DbContext.Set<T>().Find(id);
                if (del != null)
                {
                    DbContext.Set<T>().Attach(del);
                    DbContext.Entry(del).State = EntityState.Deleted;
                    await DbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public virtual async Task<bool> DeleteByListAsync<T>(List<T> LstEntity) where T : class, new()
        {
            try
            {
                bool result = false;
                try
                {
                    foreach (var item in LstEntity)
                    {
                        DbContext.Set<T>().Attach(item);
                        DbContext.Entry(item).State = EntityState.Deleted;
                    }
                    await DbContext.SaveChangesAsync();
                    result = true;
                }
                catch (Exception ex) { throw ex; }
                return result;
            }
            catch (Exception ex) { }
            return false;
        }
        #endregion

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    #endregion
}
using RefactorThis.GraphDiff;
using SurePortal.Core.Kernel.Mapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public interface IRepositoryBase<TEntity>
    {
        TEntity GetByID(object entityID);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        IList<TEntity> GetAll();
        IList<TEntity> GetAll(out int total, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> selectFields = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null, int? pageSize = null);
        IList<TEntity> GetAll(out int total, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? pageNumber = null, int? pageSize = null);
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        IList<TResult> GetByStoreProcedure<TResult>(string storeName, params object[] parameters);
        TResult ExecuteStoreProcedure<TResult>(string storeName, params object[] parameters);

        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters);

        IQueryable<TEntity> GetQueryable();

        int Count(Expression<Func<TEntity, bool>> filter);
        int Count();
    }
   
    #region IRepository Context,entity
    public interface IRepository<TContext, T> : IDisposable
        where TContext : DbContext
        where T : class
    {
        IRepositoryContext<TContext> Repo { get; set; }
        T Entity { get; set; }

        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> selector);

        T GetById(Guid Id, Expression<Func<T, object>> expressionOrder = null);
        T SelectOne(Expression<Func<T, bool>> predicate);

        T Insert(T entity);
        List<T> InsertToList(List<T> entity);
        Task<T> InsertAsync(T entity);
        Task<List<T>> InsertToListAsync(List<T> entity);

        bool Update(T updateEntity);
        bool UpdateToList(List<T> entity);
        Task<bool> UpdateAsync(T updateEntity);
        Task<bool> UpdateToListAsync(List<T> updateEntity);

        bool DeleteById(Guid id);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> DeleteByListAsync(List<T> LstEntity);
    }
    #endregion
}
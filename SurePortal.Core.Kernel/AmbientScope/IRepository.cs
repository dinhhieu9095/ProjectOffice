using RefactorThis.GraphDiff;
using SurePortal.Core.Kernel.Mapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        #region IQueryable

        /// <summary>
        /// IQueryable
        /// </summary>
        /// <typeparam name="TProject"></typeparam>
        /// <returns></returns>
        IQueryable<TProject> Query<TProject>() where TProject : class, IMapping;

        /// <summary>
        /// IQueryable
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<TEntity> GetAll();

        #endregion

        #region Get

        /// <summary>
        /// Lấy biểu ghi.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includingQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null);
        /// <summary>
        /// Lấy biểu ghi bất đồng bộ.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includingQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Lấy biểu ghi trong danh sách chỉ đọc.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includingQuery"></param>
        /// <param name="orderingQuery"></param>
        /// <param name="pagingQuery"></param>
        /// <returns></returns>
        IReadOnlyList<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            OrderingQuery<TEntity> orderingQuery = null,
            PagingQuery pagingQuery = null);
        /// <summary>
        /// Lấy biểu ghi trong danh sách chỉ đọc bất đồng bộ.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includingQuery"></param>
        /// <param name="orderingQuery"></param>
        /// <param name="pagingQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            IncludingQuery<TEntity> includingQuery = null,
            OrderingQuery<TEntity> orderingQuery = null,
            PagingQuery pagingQuery = null,
            CancellationToken cancellationToken = default);

        #endregion

        #region Add

        /// <summary>
        /// Thêm mới biểu ghi.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// Thêm mới nhiều biểu ghi.
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(List<TEntity> entities);

        #endregion

        #region Modify

        /// <summary>
        /// Hiệu chỉnh biểu ghi.
        /// </summary>
        /// <param name="entity"></param>
        void Modify(TEntity entity);
        /// <summary>
        /// Hiệu chỉnh biểu ghi, nhưng chỉ cập nhật các field được chỉ định trong param properties.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        void Modify(
            TEntity entity,
            List<Expression<Func<TEntity, object>>> properties);
        /// <summary>
        /// Hiệu chỉnh biểu ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <param name="allowDelete"></param>
        void Modify(
            TEntity entity,
            Expression<Func<IUpdateConfiguration<TEntity>, object>> expression,
            bool allowDelete = false);

        #endregion

        #region Delete

        /// <summary>
        /// Xóa biểu ghi
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// Xóa nhiều biểu ghi
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(List<TEntity> entities);
        /// <summary>
        /// Xóa biểu ghi theo điều kiện trong expression
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <param name="predicate"></param>
        void Delete(
            TEntity entity,
            Expression<Func<IUpdateConfiguration<TEntity>, object>> expression,
            Func<TEntity, bool> predicate = null);

        #endregion

        #region Count

        /// <summary>
        /// Đếm số biểu ghi.
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Đếm số biểu ghi bất đồng bộ.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Đếm số biểu ghi bất đồng bộ theo điều kiện predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        #endregion

        #region Sql Query

        /// <summary>
        /// Thực thi thủ tục bất đồng bộ.
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<T> SqlQuery<T>(string sql) where T : class, new();
        Task<List<object>> SqlQueryAsync(Type elementType, string sql, object[] parameters = null);
        #endregion
    }

    #region IRepository Context
    public interface IRepositoryContext<TContext> : IDisposable where TContext : DbContext
    {
        TContext DbContext { get; }
        IQueryable<T> GetAll<T>() where T : class, new();
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> selector) where T : class, new();

        T GetById<T>(Guid Id, Expression<Func<T, object>> expressionOrder = null) where T : class, new();
        T SelectOne<T>(Expression<Func<T, bool>> predicate) where T : class, new();

        T Insert<T>(T entity) where T : class, new();
        List<T> InsertToList<T>(List<T> entity) where T : class, new();
        Task<T> InsertAsync<T>(T insertObject) where T : class, new();
        Task<List<T>> InsertToListAsync<T>(List<T> entity) where T : class, new();

        bool Update<T>(T updateEntity) where T : class, new();
        bool UpdateToList<T>(List<T> entity) where T : class, new();
        Task<bool> UpdateAsync<T>(T updateEntity) where T : class, new();
        Task<bool> UpdateToListAsync<T>(List<T> entity) where T : class, new();

        bool DeleteById<T>(Guid id) where T : class, new();
        Task<bool> DeleteByIdAsync<T>(Guid id) where T : class, new();
        Task<bool> DeleteByListAsync<T>(List<T> LstEntity) where T : class, new();
    }
    #endregion
}
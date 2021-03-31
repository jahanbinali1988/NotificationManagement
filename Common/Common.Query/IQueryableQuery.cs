using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain;

namespace Common.Query
{
    /// <summary>
    /// query repository interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TFilter">Filter</typeparam>
    public interface IQueryableQuery<TEntity, in TFilter> where TEntity : Entity where TFilter :  IFilter
    {
        /// <summary>
        /// Get By primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Get by seqId
        /// </summary>
        /// <param name="seqId"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(long seqId);

        /// <summary>
        /// Get by shortId
        /// </summary>
        /// <param name="shortId"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(string shortId);

        /// <summary>
        /// Get list of objects which match by query
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<PagingResult<TEntity>> GetListAsync(TFilter filter);

        /// <summary>
        /// Get object by override include relations
        /// </summary>
        /// <param name="shortId"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(string shortId,
            params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get object by override include relations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Guid id,
            params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get with custom include
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        TEntity Get(Guid id, params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
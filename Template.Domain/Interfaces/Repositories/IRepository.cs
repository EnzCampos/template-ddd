using Ardalis.Specification;
using Template.Domain.API;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Template.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteByRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateByRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Mass Update Specification.
        /// This method is not transactional, so it is not possible to rollback.
        /// </summary>
        /// <returns></returns>
        ValueTask<int> UpdateWithSpecAsync(ISpecification<TEntity> spec, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> QueryWithSpecification(ISpecification<TEntity>? specification);
        Task<int> CountWithSpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<bool> AnyWithSpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<PagedList<TEntity>> PagedListAsync(IQueryable<TEntity> query, PagingParams pageParams, CancellationToken cancellationToken = default);
        Task<PagedList<TReturn>> PagedListAsync<TReturn>(IQueryable<TReturn> query, PagingParams pageParams, CancellationToken cancellationToken = default);
        Task<PagedList<TEntity>> PagedListWithSpecificationAsync(ISpecification<TEntity> specification, PagingParams pageParams, CancellationToken cancellationToken = default);
    }
}

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Template.Domain.Interfaces.Repositories;
using Template.Domain.API;
using System.Linq.Expressions;
using Template.Domain.Interfaces.Services.UnitOfWork;
using Template.Infrastructure.DatabaseContext;

namespace Template.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(TemplateDatabaseContext dbContext, IUnitOfWork unitOfWork) : IRepository<TEntity> where TEntity : class
    {
        protected readonly TemplateDatabaseContext _dbContext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);

            if (_unitOfWork.AutoSave)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(entity);
            if (_unitOfWork.AutoSave)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            } 
        }

        public async Task DeleteByRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.RemoveRange(entities);
            if (_unitOfWork.AutoSave)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (_unitOfWork.AutoSave)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateByRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.UpdateRange(entities);

            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            if (_unitOfWork.AutoSave)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        
        public async ValueTask<int> UpdateWithSpecAsync(
            ISpecification<TEntity> spec,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties,
            CancellationToken cancellationToken = default)
        {
            return await QueryWithSpecification(spec).ExecuteUpdateAsync(setProperties, cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Set<TEntity>().FindAsync(id);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public IQueryable<TEntity> QueryWithSpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
        }

        public async Task<int> CountWithSpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await QueryWithSpecification(specification).CountAsync(cancellationToken);
        }

        public async Task<bool> AnyWithSpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await QueryWithSpecification(specification).AnyAsync(cancellationToken);
        }

        public async Task<PagedList<TEntity>> PagedListAsync(IQueryable<TEntity> query, PagingParams pageParams, CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);

            var items = new List<TEntity>();

            if (count > 0)
            {
                items = await query
                    .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                    .Take(pageParams.PageSize)
                    .ToListAsync(cancellationToken);
            }

            return new PagedList<TEntity>(items, count, 1, count);
        }

        public async Task<PagedList<TReturn>> PagedListAsync<TReturn>(
            IQueryable<TReturn> query,
            PagingParams pageParams,
            CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);

            var items = new List<TReturn>();
            if (count > 0)
            {
                items = await query
                    .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                    .Take(pageParams.PageSize)
                    .ToListAsync(cancellationToken);
            }

            return new PagedList<TReturn>(items, count, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<PagedList<TEntity>> PagedListWithSpecificationAsync(ISpecification<TEntity> specification, PagingParams pageParams, CancellationToken cancellationToken = default)
        {
            var query = QueryWithSpecification(specification);
            var count = await query.CountAsync(cancellationToken);

            var items = new List<TEntity>();

            if (count > 0)
            {
                items = await query
                    .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                    .Take(pageParams.PageSize)
                    .ToListAsync(cancellationToken);
            }

            return new PagedList<TEntity>(items, count, pageParams.PageNumber, pageParams.PageSize);
        }
    }
}

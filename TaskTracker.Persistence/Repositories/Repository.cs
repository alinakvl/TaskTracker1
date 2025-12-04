
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;
//using TaskTracker.Application.Interfaces.Repositories;
//using TaskTracker.Persistence.Context;

//namespace TaskTracker.Persistence.Repositories
//{

//    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
//    {
//        protected readonly TaskTrackerDbContext _context;
//        protected readonly DbSet<TEntity> _dbSet;

//        public Repository(TaskTrackerDbContext context)
//        {
//            _context = context;
//            _dbSet = context.Set<TEntity>();
//        }

//        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
//        {
//            return await _dbSet.FindAsync(new object[] { id! }, cancellationToken);
//        }

//        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
//        {
//            return await _dbSet.ToListAsync(cancellationToken);
//        }

//        public virtual async Task<IEnumerable<TEntity>> FindAsync(
//            Expression<Func<TEntity, bool>> predicate,
//            CancellationToken cancellationToken = default)
//        {
//            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
//        }

//        public virtual async Task<TEntity?> FirstOrDefaultAsync(
//            Expression<Func<TEntity, bool>> predicate,
//            CancellationToken cancellationToken = default)
//        {
//            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
//        }

//        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
//        {
//            await _dbSet.AddAsync(entity, cancellationToken);
//            return entity;
//        }

//        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(
//            IEnumerable<TEntity> entities,
//            CancellationToken cancellationToken = default)
//        {
//            await _dbSet.AddRangeAsync(entities, cancellationToken);
//            return entities;
//        }

//        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
//        {
//            _dbSet.Update(entity);
//            return Task.CompletedTask;
//        }

//        public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
//        {
//            var entity = await GetByIdAsync(id, cancellationToken);
//            if (entity != null)
//            {
//                _dbSet.Remove(entity);
//            }
//        }

//        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
//        {
//            _dbSet.Remove(entity);
//            return Task.CompletedTask;
//        }

//        public virtual async Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(
//            int pageNumber,
//            int pageSize,
//            CancellationToken cancellationToken = default)
//        {
//            var totalCount = await _dbSet.CountAsync(cancellationToken);
//            var items = await _dbSet
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync(cancellationToken);

//            return (items, totalCount);
//        }

//        public virtual async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
//        {
//            var entity = await GetByIdAsync(id, cancellationToken);
//            return entity != null;
//        }

//        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
//        {
//            return await _dbSet.CountAsync(cancellationToken);
//        }
//    }
//}


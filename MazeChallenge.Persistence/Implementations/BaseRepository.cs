using System.Linq.Expressions;
using MazeChallenge.Domain.Context;
using MazeChallenge.Domain.Entities;
using MazeChallenge.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MazeChallenge.Persistence.Implementations
{
	public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
    {
        internal MazeDbContext _context;
        internal DbSet<TEntity> _dbSet;

        public BaseRepository(MazeDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy is not null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var createdEntity = await _dbSet.AddAsync(entity);
            return createdEntity.Entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual async Task Delete(Guid id)
        {
            TEntity entitToDetelete = await _dbSet.FindAsync(id);
            Delete(entitToDetelete);
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}


using System.Linq.Expressions;
using MazeChallenge.Domain.Entities;

namespace MazeChallenge.Persistence.Contracts
{
	public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        TEntity GetEntity(Guid id, string includeProperties = "");
        Task<TEntity> FindAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(Guid id);
    }
}


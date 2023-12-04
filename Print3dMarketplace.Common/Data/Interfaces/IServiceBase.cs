using System.Linq.Expressions;

namespace Print3dMarketplace.Common.Data.Interfaces;

public interface IServiceBase<TEntity> where TEntity : class
{
	Task<TEntity> GetByIdAsync(int id);
	Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
	Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
	Task AddAsync(TEntity entity);
	Task AddRangeAsync(IEnumerable<TEntity> entities);
	void Remove(TEntity entity);
	void RemoveRange(IEnumerable<TEntity> entities);
	void Update(TEntity entity);
}

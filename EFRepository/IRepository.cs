using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntityT<TKey>
    {
        Task<TEntity> GetAsync(TKey ID);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(params TEntity[] entities);

        void UpdateAsync(TEntity newEntity);
        void UpdateRangeAsync(params TEntity[] entities);

        Task<bool> RemoveAsync(TEntity newEntity);

        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task SaveChangesAsync();
    }
}

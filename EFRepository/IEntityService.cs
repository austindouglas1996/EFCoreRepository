using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    /// <summary>
    /// Represents the basic contract needed for an Entity service that handles simple CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntityService<TEntity, TKey> : IDisposable
    {
        Task<TEntity> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entityDto);
        void UpdateAsync(TEntity entityDto);
        Task RemoveAsync(TKey id);
        Task SaveChangesAsync();
    }
}

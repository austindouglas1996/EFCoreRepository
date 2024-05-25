using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    /// <summary>
    /// Represents a base service class for handling CRUD operations when interacting with entities. Helpful with Blazor web pages.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityServiceBase<TRepository, TEntity, TKey> : IEntityService<TEntity, TKey>
        where TEntity : class, IEntityT<TKey>
        where TRepository : class, IRepository<TEntity, TKey>
    {
        protected TRepository Repository
        {
            get => _repository;
        }
        private readonly TRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityServiceBase{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="repository"></param>
        protected EntityServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Calls <see cref="IRepository{TEntity, TKey}.GetAllAsync"/> method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(TKey id) => await _repository.GetAsync(id);

        /// <summary>
        /// Calls <see cref="IRepository{TEntity, TKey}.GetAllAsync"/> method.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();

        /// <summary>
        /// Calls <see cref="IRepository{TEntity, TKey}.AddAsync(TEntity)"/> method.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
        }

        /// <summary>
        /// Calls <see cref="IRepository{TEntity, TKey}.UpdateAsync(TEntity)"/> method.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void UpdateAsync(TEntity entity)
        {
            _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Calls <see cref="IRepository{TEntity, TKey}.RemoveAsync(TEntity)"/> method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(TKey id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity != null)
            {
                await _repository.RemoveAsync(entity);
            }
        }

        /// <summary>
        /// Calls <see cref="IRepository{TEntity, TKey}.SaveChangesAsync"/> method.
        /// </summary>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync() => await _repository.SaveChangesAsync();

        /// <summary>
        /// Dispose of the data.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
        }
    }

}

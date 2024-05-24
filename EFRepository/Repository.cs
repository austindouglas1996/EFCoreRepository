#pragma warning disable CS8603
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    /// <summary>
    /// An abstract generic repository implementation that provides asynchrous data access methods when working with an Entity Framework source. Serves as the base class
    /// when handling repository items.
    /// </summary>
    /// <typeparam name="TDbContext">The database context that belongs to this repository.</typeparam>
    /// <typeparam name="TEntity">The entity to include in this repository.</typeparam>
    /// <typeparam name="TKey">Required for filtering methods like <see cref="FindAsync(Expression{Func{TEntity, bool}})"/>.</typeparam>
    public abstract class Repository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntityT<TKey>
    {
        /// <summary>
        /// Database context that this repository is defined in.
        /// </summary>
        public TDbContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TDbContext, TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Repository(TDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("Context is null.");

            this.Context = context;
        }

        /// <summary>
        /// Gets an entity based on ID async.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(TKey ID) => await Context.Set<TEntity>().FindAsync(ID);

        /// <summary>
        /// Gets a list of all entities within the repository async.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await this.Context.Set<TEntity>().ToListAsync();

        /// <summary>
        /// Finds a group of entities based on a predicate function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await Context.Set<TEntity>().Where(predicate).ToListAsync();

        /// <summary>
        /// Returns the first or default of a <see cref="TEntity"/> based on an expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        /// <summary>
        /// Returns one entity or default value based on a predicate function.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);

        /// <summary>
        /// Adds a new child to the repository.
        /// </summary>
        /// <param name="entity"></param>
        public async Task AddAsync(TEntity entity) => await this.Context.Set<TEntity>().AddAsync(entity);

        /// <summary>
        /// Adds a collection of entities to the repository.
        /// </summary>
        /// <param name="entities"></param>
        public async Task AddRangeAsync(params TEntity[] entities) => await this.Context.Set<TEntity>().AddRangeAsync(entities);

        /// <summary>
        /// Edit an entity within the repository async.
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateAsync(TEntity newEntity) => this.Context.Update(newEntity);

        /// <summary>
        /// Edit a range of entities within the repository async.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public void UpdateRangeAsync(params TEntity[] entities) => this.Context.Set<TEntity>().UpdateRange(entities);

        /// <summary>
        /// Remove an entity from the repository.
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(TEntity oldEntity)
        {
            if (await this.GetAsync(oldEntity.Id) == null)
                return false;

            this.Context.Set<TEntity>().Remove(oldEntity);
            return true;
        }

        /// <summary>
        /// Returns an array of elements based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate) => await Context.Set<TEntity>().Where(predicate).ToListAsync();

        /// <summary>
        /// Allows you to save the collection of entities.
        /// </summary>
        /// <remarks>Not supported in a List repository.</remarks>
        public async Task SaveChangesAsync() => await this.Context.SaveChangesAsync();
    }
}

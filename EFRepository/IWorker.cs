using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    /// <summary>
    /// Helps define the contract for a worker instance that interacts with a <see cref="DbContext"/>. This type of object that create concrete instances
    /// of <see cref="IRepository{TEntity, TKey}"/> instances.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IWorker<TContext> : IDisposable
        where TContext : DbContext
    {
        public TContext Context { get; }
        public Task<bool> SaveChanges();
    }
}

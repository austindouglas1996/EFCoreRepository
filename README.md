---

# EFCoreRepository
A simple implementation of a CRUD application using Entity Framework Core. This guide helps you design simple apps for creating CRUD repositories when programming in C#.

## Data Access
The approach to working with the DbContext in this project is intentionally ambiguous. This design choice encourages you to refer to the official Microsoft documentation for creating a proper DbContext. The primary focus of this project is on routing and organizing the data as it is used throughout the project, rather than the specifics of configuring the DbContext. This helps instead of needing to work with certain database types.

## Repository Layer
The repository layer uses a simple `IList<T>` asynchronous repository for managing data. The `IRepository<TEntity, TKey>` interface defines the standard CRUD operations and other query methods. Concrete implementations of this interface, such as the abstract `Repository` , provide the actual data access logic using Entity Framework Core. These repositories encapsulate the data access logic, promoting a clean separation of concerns and making the data layer easier to maintain and test.

## Service Layer
The service layer leverages `IWorker` to manage instances of `IRepository` implementations. The `IWorker` interface acts as a unit of work, coordinating the operations across multiple repositories. Additionally, the `EntityService<TEntity, TKey>` class in the service layer facilitates managing CRUD operations for specific entities. This service layer abstracts the business logic from the data access logic, ensuring a clean architecture and promoting reusability and maintainability.

---

### Examples

#### Repository Layer
The repository layer abstracts the data access logic, providing a simple and consistent interface for CRUD operations. The `IRepository<TEntity, TKey>` interface defines methods such as `GetAsync`, `GetAllAsync`, `AddAsync`, `Update`, `Remove`, `SaveChangesAsync`, and query methods like `WhereAsync`. Concrete implementations of this interface use Entity Framework Core to perform these operations asynchronously, ensuring non-blocking data access. Which is important whaen working with projects like Blazor, or ASP.NET.

**Example IRepository<TEntity,TKey>**
```csharp
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
```

**Included is a simple concrete abstract instance of IRepsoitory, Repository**
</br> A simple concrete instance of IRepository that uses Entity Framework default asynchrous methods for execution for perfomring the basic CRUD operations.
```csharp
    public abstract class Repository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntityT<TKey>
    {
        public async Task<TEntity> GetAsync(TKey ID) => await Context.Set<TEntity>().FindAsync(ID);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await this.Context.Set<TEntity>().ToListAsync();
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await Context.Set<TEntity>().Where(predicate).ToListAsync();
        ...
```

**Example Implementation using concrete Repository:**
</br>Creating new repositories for easy extension functions to fit specific entity needs without overcomplicating the solution.
```csharp
    public class UserRepository(ExampleContext context) : Repository<ExampleContext, User, int>(context)
    {
        public async Task<bool> IsActiveAccountWithEmail(string emailAddress)
        {
            var foundUser = await base.FirstOrDefaultAsync(u => u.email == emailAddress);
            return foundUser != null;
        }
    }
```

#### Service Layer
The service layer is responsible for the business logic of the application. It uses `IWorker` to manage repository instances and provides higher-level operations that may involve multiple repositories or complex business rules. The `EntityService<TEntity, TKey>` class encapsulates the common CRUD operations for a specific entity type, making it easier to manage the lifecycle of entities and allowing for faster creation of entities.

```csharp
    public interface IEntityService<TEntity, TKey> : IDisposable
    {
        Task<TEntity> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entityDto);
        void UpdateAsync(TEntity entityDto);
        Task RemoveAsync(TKey id);
        Task SaveChangesAsync();
    }
```

Included is a simple concrete abstract instance of IEntityService, EntityServiceBase which helps with quickly creating mew service instances for repositories. Providing virtual operations that perform the Repository function.
```csharp
    public abstract class EntityServiceBase<TRepository, TEntity, TKey> : IEntityService<TEntity, TKey>
        where TEntity : class, IEntityT<TKey>
        where TRepository : class, IRepository<TEntity, TKey>
    {
        private readonly TRepository _repository;

        public virtual async Task<TEntity> GetAsync(TKey id) => await _repository.GetAsync(id);
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();
        ...
```

**Example implementation using concrete EntityServiceBase**
```csharp
    public class UserService(UserRepository repository) : EntityServiceBase<UserRepository, User, int>(repository)
    {
        public override async Task AddAsync(User entity)
        {
            if (await Repository.IsActiveAccountWithEmail(entity.Email))
            {
                throw new ArgumentException("Error: An account with that email already exists.");
            }

            await base.AddAsync(entity);
        }
    }
```

### Summary
This project demonstrates a clean and organized approach to building a CRUD application using Entity Framework Core. It highlights the importance of separating data access logic (repository layer) from business logic (service layer) and encourages best practices for managing dependencies and data operations in a scalable and maintainable way. This project also services as an easy way to quickly create prototype applications. Several examples are also included to help with understanding how to better utilize.

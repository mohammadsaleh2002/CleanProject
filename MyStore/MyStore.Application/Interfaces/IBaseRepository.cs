using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces
{
    // <TEntity> is a generic type parameter. 
    // It means this interface can work for any entity (like Product, Category, etc.)
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        // Async method to get an entity by its ID.
        Task<TEntity?> GetByIdAsync(int id);

        // Async method to get all entities.
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Returns IQueryable to allow building complex queries
        // in the Application layer (like filtering, sorting)
        // before executing them in the database.
        IQueryable<TEntity> GetQueryable();

        // Synchronous methods for marking changes in memory (Unit of Work pattern).
        // These do NOT call the database directly.
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
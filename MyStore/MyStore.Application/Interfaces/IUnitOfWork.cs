using MyStore.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces
{
    // The Unit of Work interface manages all repositories
    // and handles the transaction (saving changes).
    // It implements IDisposable to properly dispose of the DbContext.
    public interface IUnitOfWork : IDisposable
    {
        // Properties to access each specific repository
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        // We can add IUserRepository etc. here later

        // The method that commits all changes made within this unit of work
        // to the database, in a single transaction.
        IBaseRepository<User> Users { get; } 
        Task<int> CompleteAsync();

   
    }
}
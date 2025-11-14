using MyStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces
{
    // This interface inherits all the generic CRUD methods for the Order entity.
    public interface IOrderRepository : IBaseRepository<Order>
    {
        // Example of a specific method for orders
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
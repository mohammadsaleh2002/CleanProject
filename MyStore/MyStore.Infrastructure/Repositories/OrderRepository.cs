using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using MyStore.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.Repositories
{
    // We inherit from the generic BaseRepository for Order
    // and implement the specific IOrderRepository
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        // We pass the context to the base class constructor
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementation of the specific method
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            // We can access _context directly (it's 'protected' in BaseRepository)
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems) // Eagerly load the related OrderItems
                .ThenInclude(oi => oi.Product) // Then load the Product for each OrderItem
                .ToListAsync();
        }
    }
}
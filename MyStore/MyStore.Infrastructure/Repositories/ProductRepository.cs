using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using MyStore.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.Repositories
{
    // We inherit from the generic BaseRepository for Product
    // and implement the specific IProductRepository
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        // We pass the context to the base class constructor
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementation of the specific method
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // We can access _context directly (it's 'protected' in BaseRepository)
            // or use GetQueryable()
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
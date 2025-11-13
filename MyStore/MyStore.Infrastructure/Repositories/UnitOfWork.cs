using MyStore.Application.Interfaces;
using MyStore.Infrastructure.Data;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // These will hold the instances of our repositories
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // Create instances of the repositories, passing the context
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Orders = new OrderRepository(_context);
        }

        // This is the method that saves all changes to the database
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // This disposes the context when the Unit of Work is disposed
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using MyStore.Infrastructure.Data;

namespace MyStore.Infrastructure.Repositories
{
    // We inherit from the generic BaseRepository for Category
    // and implement the specific ICategoryRepository
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        // We pass the context to the base class constructor
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            // No specific methods to implement yet
            // All basic CRUD operations are inherited from BaseRepository
        }
    }
}
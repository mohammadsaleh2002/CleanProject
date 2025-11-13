using MyStore.Domain.Entities;

namespace MyStore.Application.Interfaces
{
    // This interface inherits all the generic CRUD methods for the Category entity.
    // We can add category-specific methods here later if needed.
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        // Currently no specific methods needed.
        // If we needed a method like "GetCategoriesWithMostProducts()",
        // we would define it here.
    }
}
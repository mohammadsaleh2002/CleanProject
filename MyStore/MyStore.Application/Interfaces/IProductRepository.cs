using MyStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces
{
    // This interface inherits all the generic CRUD methods from IBaseRepository
    // for the Product entity.
    public interface IProductRepository : IBaseRepository<Product>
    {
        // We can add specific, complex queries related to Products here.
        // For example:
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
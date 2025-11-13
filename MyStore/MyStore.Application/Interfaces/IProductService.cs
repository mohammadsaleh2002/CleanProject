using MyStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces
{
    // This interface defines the contract for business logic related to products.
    // The Controller (in the Web layer) will interact with this service.
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        // In a real app, we would use DTOs (Data Transfer Objects) here
        // instead of the raw entity, but we use Product for simplicity.
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
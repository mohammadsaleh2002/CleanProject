using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Application.Services
{
    public class ProductService : IProductService
    {
        // We inject the Unit of Work, not the individual repositories.
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            // Access the specific repository via the Unit of Work
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.Products.GetProductsByCategoryAsync(categoryId);
        }

        public async Task AddProductAsync(Product product)
        {
            // Business logic can go here (e.g., validation)
            // if (product.Price < 0) throw new Exception("Price cannot be negative.");

            // 1. Add the product to the repository (in memory)
            _unitOfWork.Products.Add(product);

            // 2. Commit the change to the database
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            // 1. Mark the product as updated (in memory)
            _unitOfWork.Products.Update(product);

            // 2. Commit the change
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            // 1. Find the product
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product != null)
            {
                // 2. Mark for deletion (in memory)
                _unitOfWork.Products.Delete(product);

                // 3. Commit the deletion
                await _unitOfWork.CompleteAsync();
            }
            // else: maybe throw a NotFoundException
        }
    }
}
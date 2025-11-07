using CleanProject.Application.Interfaces;
using CleanProject.Domain.Interfaces;
using CleanProject.Domain.Models;
using CleanProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProject.Application.Services
{
    public class ProductService : IProductService

    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void AddProduct(CreateProductViewModels product)
        {
            productRepository.Add(product);
        }

        public void DeleteAllProduct()
        {
            productRepository.DeleteAll();
        }

        public void DeleteProduct(Product product)
        {
            productRepository.Delete(product);
        }

        public bool DeleteProductById(int Id)
        {
            var result = productRepository.Delete(Id);
            return result;
        }

        public bool EditProduct(EditProductViewModels product)
        {
            var result = productRepository.Edit(product);
            return result;
           // return productRepository.Edit(product);
           }

        public Product Get(int Id)
        {
            return productRepository.Get(Id);
        }

        public IEnumerable<ProductListViewModels> GetAll()
        {
            var result = productRepository.GetAll();
            return result;
        }
    }
}

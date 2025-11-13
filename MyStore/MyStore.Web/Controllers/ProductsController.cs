using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;

namespace MyStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Products
    public class ProductsController : ControllerBase
    {
        // We inject the Service, not the UnitOfWork or Repository
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products); // Returns 200 OK + JSON data
        }

        // GET: /api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Returns 404 Not Found
            }
            return Ok(product); // Returns 200 OK + JSON data
        }

        // POST: /api/Products
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            // Note: In a real app, use a DTO (Data Transfer Object) here
            // instead of the raw entity to prevent over-posting attacks.

            await _productService.AddProductAsync(product);

            // Return 201 Created status with the new product
            // and a 'Location' header pointing to the new resource.
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: /api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); // Returns 400 Bad Request
            }

            // In a real app, you'd check if the product exists first
            await _productService.UpdateProductAsync(product);

            return NoContent(); // Returns 204 No Content (Success, but no data to return)
        }

        // DELETE: /api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Returns 404 Not Found
            }

            await _productService.DeleteProductAsync(id);

            return NoContent(); // Returns 204 No Content
        }
    }
}
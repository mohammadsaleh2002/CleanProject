// File: MyStore.Web/Controllers/StoreController.cs
using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using System.Threading.Tasks;

namespace MyStore.Web.Controllers
{
    public class StoreController : Controller
    {
        // We inject the same service as the API controller
        private readonly IProductService _productService;

        public StoreController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Store/Index or /Store
        public async Task<IActionResult> Index()
        {
            // 1. Get all products from the service
            var products = await _productService.GetAllProductsAsync();

            // 2. Pass the list of products to the View
            return View(products);
        }

        // GET: /Store/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // --- Create ---

        // GET: /Store/Create
        // This method just displays the empty form
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // We need to fix the Category nullable issue here too
                product.Category = null;

                await _productService.AddProductAsync(product);

                // Redirect back to the main list after success
                return RedirectToAction(nameof(Index));
            }

            // If model is not valid, return to the form with error messages
            return View(product);
        }

        // --- Delete (Show Confirmation) ---

        // GET: /Store/Delete/5
        // This method finds the product and shows the "Are you sure?" page
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Pass the product to the Delete.cshtml view
            return View(product);
        }

        // --- Delete (Execute) ---

        // POST: /Store/Delete/5
        // This method is called when the user clicks "Delete" on the confirmation page
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);

            // Redirect back to the main list
            return RedirectToAction(nameof(Index));
        }
    }
}
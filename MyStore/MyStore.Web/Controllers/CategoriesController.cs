using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using Microsoft.EntityFrameworkCore; 
namespace MyStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Categories
    public class CategoriesController : ControllerBase
    {
        // For simplicity, we inject IUnitOfWork directly.
        // In a real app, you would create an ICategoryService first.
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // We use GetAllAsync() from the base repository
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return Ok(categories);
        }

        // GET: /api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: /api/Categories
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            // We must set Products to null or empty to avoid EF errors
            // because the JSON won't contain it.
            category.Products = new List<Product>();

            _unitOfWork.Categories.Add(category);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
    }
}
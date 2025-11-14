using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Orders
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /api/Orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // We use the specific repository method to include related data
            var orders = await _unitOfWork.Orders.GetQueryable()
                .Include(o => o.OrderItems) // Include the order items
                .ThenInclude(oi => oi.Product) // Include the product for each item
                .ToListAsync();

            return Ok(orders);
        }

        // GET: /api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Find the order and include its related items and products
            var order = await _unitOfWork.Orders.GetQueryable()
                .Where(o => o.Id == id)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // GET: /api/Orders/User/1
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            // Use the specific repository method we built
            var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // POST: /api/Orders
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            // In a real app, this logic would be in an IOrderService
            // and would be much more complex (e.g., calculating TotalAmount).

            // We just set the date and ensure lists are not null
            order.OrderDate = DateTime.UtcNow;
            order.User = null; // Avoid EF trying to create a new user

            _unitOfWork.Orders.Add(order);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
    }
}
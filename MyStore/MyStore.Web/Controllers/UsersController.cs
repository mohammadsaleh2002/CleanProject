using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Interfaces;
using MyStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Users
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /api/Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // We use GetQueryable() and must call ToListAsync()
            var users = await _unitOfWork.Users.GetQueryable().ToListAsync();
            return Ok(users);
        }

        // GET: /api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: /api/Users
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            // WARNING: In a real app, you would NOT pass a PasswordHash.
            // You would pass a password, hash it here (in a service),
            // and then save. We are only doing this for API simplicity.
            user.Orders = new List<Order>();

            _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
    }
}
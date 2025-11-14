using MyStore.Domain.Entities;
using System.Threading.Tasks;

namespace MyStore.Application.Interfaces
{
    // Interface for Manual Authentication Logic
    public interface IUserService
    {
        // Registration: Creates the user and hashes the password
        Task<bool> RegisterUserAsync(User user, string password);

        // Login: Verifies the password and returns the User object if successful
        Task<User?> AuthenticateUserAsync(string email, string password);

        // Utility: Find user by ID/Email
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
    }
}
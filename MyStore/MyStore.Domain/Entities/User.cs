using System.Collections.Generic;

namespace MyStore.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // We store a hash, never the plain password
        public string Address { get; set; }

        // Navigation Property
        // A user can place multiple orders.
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
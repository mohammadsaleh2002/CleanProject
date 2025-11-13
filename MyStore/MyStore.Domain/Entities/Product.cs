using System.Collections.Generic;

namespace MyStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Foreign Key for Category
        public int CategoryId { get; set; }

        // Navigation Property
        // Each product belongs to one Category.
        public Category? Category { get; set; }

        // Navigation Property
        // Each product can be part of many OrderItems.
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
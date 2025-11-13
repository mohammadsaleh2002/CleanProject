using System.Collections.Generic;

namespace MyStore.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        // A category can have multiple products.
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
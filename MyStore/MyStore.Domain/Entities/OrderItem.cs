namespace MyStore.Domain.Entities
{
    // This is a "join table" or "linking table"
    // It manages the Many-to-Many relationship between Order and Product.
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Price at the time of purchase

        // Foreign Key for Order
        public int OrderId { get; set; }
        // Navigation Property
        public Order Order { get; set; }

        // Foreign Key for Product
        public int ProductId { get; set; }
        // Navigation Property
        public Product Product { get; set; }
    }
}
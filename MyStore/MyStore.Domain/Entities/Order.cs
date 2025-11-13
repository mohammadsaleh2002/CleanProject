// File: MyStore.Domain/Entities/Order.cs
using System;
using System.Collections.Generic;

namespace MyStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign Key for User
        public int UserId { get; set; }

        // Navigation Property
        // Each order belongs to one User.
        public User User { get; set; }

        // Navigation Property
        // Each order contains multiple OrderItems.
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
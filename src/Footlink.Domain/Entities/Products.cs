

using System;
using System.Collections.Generic;


namespace Footlink.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
            // Navigazione
    public Company Company { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    }
}
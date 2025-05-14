

using System;
using System.Collections.Generic;


namespace Footlink.Domain.Entities
{
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    // Navigazione
    public Order Order { get; set; }
    public Product Product { get; set; }
}
}
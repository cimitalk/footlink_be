

using System;
using System.Collections.Generic;

namespace Footlink.Domain.Entities
{
public class Order
{
    public int Id { get; set; }
    public int BuyerCompanyId { get; set; }
    public int SupplierCompanyId { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
     // Navigazione
    public Company BuyerCompany { get; set; }
    public Company SupplierCompany { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<ProductionProgress> ProductionProgresses { get; set; }
    public ICollection<Shipment> Shipments { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}
}
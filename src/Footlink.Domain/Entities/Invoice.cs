

using System;
using System.Collections.Generic;


namespace Footlink.Domain.Entities
{
public class Invoice
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string InvoiceNumber { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string XmlFile { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
//Navigazione
    public Order Order { get; set; }
}
}
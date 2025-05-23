

using System;
using System.Collections.Generic;


namespace Footlink.Domain.Entities
{
public class ProductionProgress
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Stage { get; set; }

    public string Description { get; set; }

    public int ProgressPercent { get; set; }

    public DateTime UpdatedAt { get; set; }

        // Navigazione
    public Order Order { get; set; }
}
}

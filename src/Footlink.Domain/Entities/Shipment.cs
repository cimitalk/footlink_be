namespace Footlink.Domain.Entities
{
public class Shipment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string TrackingNumber { get; set; }

    public string Carrier { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? DeliveryEstimate { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public Order Order { get; set; }
}
}
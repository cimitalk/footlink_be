namespace Footlink.Domain.Entities
{
public class Orders
{
    public int Id { get; set; }
    public int BuyerCompanyId { get; set; }
    public int SupplierCompanyId { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
}
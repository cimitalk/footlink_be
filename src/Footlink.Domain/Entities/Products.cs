namespace Footlink.Domain.Entities
{
    public class Products
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
    }
}
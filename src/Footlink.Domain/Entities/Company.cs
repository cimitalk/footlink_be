namespace Footlink.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public string GroupName { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

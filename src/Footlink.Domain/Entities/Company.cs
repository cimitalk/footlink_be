

using System;
using System.Collections.Generic;


namespace Footlink.Domain.Entities
{
    public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string VatNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigazione
    public ICollection<User> Users { get; set; }
    public ICollection<Product> Products { get; set; }
}

    
}


using System;
using System.Collections.Generic;


namespace Footlink.Domain.Entities
{
    public class User
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    // Navigazione
    public Company Company { get; set; }
}
}

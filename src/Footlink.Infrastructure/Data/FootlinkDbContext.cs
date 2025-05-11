using Microsoft.EntityFrameworkCore;
using Footlink.Domain.Entities;

namespace Footlink.Infrastructure.Data
{
    public class FootlinkDbContext : DbContext
    {
        public FootlinkDbContext(DbContextOptions<FootlinkDbContext> options)
            : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
                entity.Property(c => c.VatNumber).HasColumnName("vat_number").IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.FirstName).HasColumnName("first_name").IsRequired().HasMaxLength(255);
                entity.Property(u => u.LastName).HasColumnName("last_name").IsRequired().HasMaxLength(255);
                entity.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(u => u.CompanyId).HasColumnName("company_id");

                entity.HasOne(u => u.Company)
                      .WithMany(c => c.Users)
                      .HasForeignKey(u => u.CompanyId);
            });
        }
    }
}

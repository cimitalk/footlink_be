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
    // companies
    modelBuilder.Entity<Company>(entity =>
    {
        entity.ToTable("companies");

        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id).HasColumnName("id");
        entity.Property(c => c.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
        entity.Property(c => c.VatNumber).HasColumnName("vat_number").IsRequired().HasMaxLength(50);
        entity.Property(c => c.Address).HasColumnName("address");
        entity.Property(c => c.City).HasColumnName("city").HasMaxLength(100);
        entity.Property(c => c.Province).HasColumnName("province").HasMaxLength(50);
        entity.Property(c => c.PostalCode).HasColumnName("postal_code").HasMaxLength(20);
        entity.Property(c => c.Country).HasColumnName("country").HasMaxLength(100);
        entity.Property(c => c.Email).HasColumnName("email").HasMaxLength(255);
        entity.Property(c => c.Phone).HasColumnName("phone").HasMaxLength(50);
        entity.Property(c => c.Website).HasColumnName("website").HasMaxLength(255);
        entity.Property(c => c.CreatedAt).HasColumnName("created_at");

        entity.HasMany(c => c.Users)
              .WithOne(u => u.Company)
              .HasForeignKey(u => u.CompanyId);

        entity.HasMany(c => c.Products)
              .WithOne(p => p.Company)
              .HasForeignKey(p => p.CompanyId);
    });

    // users
    modelBuilder.Entity<User>(entity =>
    {
        entity.ToTable("users");

        entity.HasKey(u => u.Id);
        entity.Property(u => u.Id).HasColumnName("id");
        entity.Property(u => u.CompanyId).HasColumnName("company_id");
        entity.Property(u => u.FirstName).HasColumnName("first_name").HasMaxLength(100);
        entity.Property(u => u.LastName).HasColumnName("last_name").HasMaxLength(100);
        entity.Property(u => u.Email).HasColumnName("email").HasMaxLength(255);
        entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
        entity.Property(u => u.Role).HasColumnName("role").HasMaxLength(50);
        entity.Property(u => u.IsActive).HasColumnName("is_active");
        entity.Property(u => u.CreatedAt).HasColumnName("created_at");
    });

    // products
    modelBuilder.Entity<Product>(entity =>
    {
        entity.ToTable("products");

        entity.HasKey(p => p.Id);
        entity.Property(p => p.Id).HasColumnName("id");
        entity.Property(p => p.CompanyId).HasColumnName("company_id");
        entity.Property(p => p.Name).HasColumnName("name").HasMaxLength(255);
        entity.Property(p => p.Description).HasColumnName("description");
        entity.Property(p => p.Sku).HasColumnName("sku").HasMaxLength(100);
        entity.Property(p => p.Category).HasColumnName("category").HasMaxLength(100);
        entity.Property(p => p.UnitPrice).HasColumnName("unit_price");
        entity.Property(p => p.Currency).HasColumnName("currency").HasMaxLength(10);
        entity.Property(p => p.CreatedAt).HasColumnName("created_at");
    });

    // orders
    modelBuilder.Entity<Order>(entity =>
    {
        entity.ToTable("orders");

        entity.HasKey(o => o.Id);
        entity.Property(o => o.Id).HasColumnName("id");
        entity.Property(o => o.BuyerCompanyId).HasColumnName("buyer_company_id");
        entity.Property(o => o.SupplierCompanyId).HasColumnName("supplier_company_id");
        entity.Property(o => o.Status).HasColumnName("status").HasMaxLength(50);
        entity.Property(o => o.OrderDate).HasColumnName("order_date");
        entity.Property(o => o.DeliveryDate).HasColumnName("delivery_date");
        entity.Property(o => o.Notes).HasColumnName("notes");
        entity.Property(o => o.CreatedAt).HasColumnName("created_at");

        entity.HasOne(o => o.BuyerCompany)
              .WithMany()
              .HasForeignKey(o => o.BuyerCompanyId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.SupplierCompany)
              .WithMany()
              .HasForeignKey(o => o.SupplierCompanyId)
              .OnDelete(DeleteBehavior.Restrict);
    });

    // order_items
    modelBuilder.Entity<OrderItem>(entity =>
    {
        entity.ToTable("order_items");

        entity.HasKey(oi => oi.Id);
        entity.Property(oi => oi.Id).HasColumnName("id");
        entity.Property(oi => oi.OrderId).HasColumnName("order_id");
        entity.Property(oi => oi.ProductId).HasColumnName("product_id");
        entity.Property(oi => oi.Quantity).HasColumnName("quantity");
        entity.Property(oi => oi.UnitPrice).HasColumnName("unit_price");
        entity.Property(oi => oi.TotalPrice).HasColumnName("total_price");

        entity.HasOne(oi => oi.Order)
              .WithMany(o => o.OrderItems)
              .HasForeignKey(oi => oi.OrderId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(oi => oi.Product)
              .WithMany(p => p.OrderItems)
              .HasForeignKey(oi => oi.ProductId);
    });

    // production_progress
    modelBuilder.Entity<ProductionProgress>(entity =>
    {
        entity.ToTable("production_progress");

        entity.HasKey(pp => pp.Id);
        entity.Property(pp => pp.Id).HasColumnName("id");
        entity.Property(pp => pp.OrderId).HasColumnName("order_id");
        entity.Property(pp => pp.Stage).HasColumnName("stage").HasMaxLength(100);
        entity.Property(pp => pp.Description).HasColumnName("description");
        entity.Property(pp => pp.ProgressPercent).HasColumnName("progress_percent");
        entity.Property(pp => pp.UpdatedAt).HasColumnName("updated_at");

        entity.HasOne(pp => pp.Order)
              .WithMany(o => o.ProductionProgresses)
              .HasForeignKey(pp => pp.OrderId);
    });

    // shipments
    modelBuilder.Entity<Shipment>(entity =>
    {
        entity.ToTable("shipments");

        entity.HasKey(s => s.Id);
        entity.Property(s => s.Id).HasColumnName("id");
        entity.Property(s => s.OrderId).HasColumnName("order_id");
        entity.Property(s => s.TrackingNumber).HasColumnName("tracking_number").HasMaxLength(100);
        entity.Property(s => s.Carrier).HasColumnName("carrier").HasMaxLength(100);
        entity.Property(s => s.ShippedDate).HasColumnName("shipped_date");
        entity.Property(s => s.DeliveryEstimate).HasColumnName("delivery_estimate");
        entity.Property(s => s.Status).HasColumnName("status").HasMaxLength(50);
        entity.Property(s => s.CreatedAt).HasColumnName("created_at");

        entity.HasOne(s => s.Order)
              .WithMany(o => o.Shipments)
              .HasForeignKey(s => s.OrderId);
    });

    // invoices
    modelBuilder.Entity<Invoice>(entity =>
    {
        entity.ToTable("invoices");

        entity.HasKey(i => i.Id);
        entity.Property(i => i.Id).HasColumnName("id");
        entity.Property(i => i.OrderId).HasColumnName("order_id");
        entity.Property(i => i.InvoiceNumber).HasColumnName("invoice_number").HasMaxLength(100);
        entity.Property(i => i.IssueDate).HasColumnName("issue_date");
        entity.Property(i => i.TotalAmount).HasColumnName("total_amount");
        entity.Property(i => i.XmlFile).HasColumnName("xml_file");
        entity.Property(i => i.Status).HasColumnName("status").HasMaxLength(50);
        entity.Property(i => i.CreatedAt).HasColumnName("created_at");

        entity.HasOne(i => i.Order)
              .WithMany(o => o.Invoices)
              .HasForeignKey(i => i.OrderId);
    });
}

    }
}

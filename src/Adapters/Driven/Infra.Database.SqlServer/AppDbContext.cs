using Infra.Database.SqlServer.Employee.Configuration;
using Infra.Database.SqlServer.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer;

public class AppDbContext : DbContext
{
    // public DbSet<Customer> Customers { get; set; }
    //
    // public DbSet<Order> Orders { get; set; }
    //
    // public DbSet<OrderItem> OrderItems { get; set; }
    //
    // public DbSet<Payment> Payments { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ImageProduct> ImageProducts { get; set; }
    public DbSet<Domain.Employee.Entities.Employee> Employees { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImageProduct>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId);

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}
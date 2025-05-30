using Domain.Customer.Entities;
using Domain.Payments.Entities;
using Infra.Database.SqlServer.Customers.Configuration;
using Infra.Database.SqlServer.Employee.Configuration;
using Infra.Database.SqlServer.Order.Configuration;
using Infra.Database.SqlServer.Payments.Configuration;
using Infra.Database.SqlServer.Products.Configuration;
using Infra.Database.SqlServer.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<ImageProduct> ImageProducts { get; set; }
    public DbSet<Domain.Employee.Entities.Employee> Employees { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Domain.Order.Entities.Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageProductsConfiguration());
        modelBuilder.ApplyConfiguration(new ProductsConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CustomersConfiguration());
    }
}
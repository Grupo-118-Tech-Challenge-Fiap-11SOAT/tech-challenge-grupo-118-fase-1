using Microsoft.EntityFrameworkCore;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Customers.Configuration;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Customers.Entities;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Configuration;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Configuration;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Entities;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Payments.Configuration;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Payments.Entities;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Configuration;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database;

public class CleanArchDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order.Entities.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ImageProduct> ImageProducts { get; set; }
    public DbSet<Employee.Entities.Employee> Employees { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; }

    public CleanArchDbContext(DbContextOptions<CleanArchDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageProductsConfiguration());
        modelBuilder.ApplyConfiguration(new ProductsConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new CustomersConfiguration());
    }
}
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer;

public class AppDbContext : DbContext
{
    
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Employee> Employees { get; set; }
    
    public DbSet<ImageProduct> ImageProducts { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<Payment> Payments { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}
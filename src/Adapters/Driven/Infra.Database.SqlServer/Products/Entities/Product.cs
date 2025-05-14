using Domain.Products.ValueObjects;

namespace Infra.Database.SqlServer.Products.Entities;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    
    public ProductType Category { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
    
    public List<ImageProduct> Images { get; set; } = new List<ImageProduct>();
}
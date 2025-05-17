using Domain;
using Domain.Products.ValueObjects;

namespace Infra.Database.SqlServer.Products.Entities;

public class Product : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ProductType Category { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public List<ImageProduct> Images { get; set; } = new List<ImageProduct>();

    public override Domain.Products.Entities.Product ToDomain()
    {
        return new Domain.Products.Entities.Product(this.Name,
            this.Description,
            this.Category,
            this.Price,
            this.IsActive,
            this.Id);
    }

    public override void DomainToEntity(BaseDomain domain)
    {
        var product = (Domain.Products.Entities.Product)domain;

        this.Name = product.Name;
        this.Description = product.Description;
        this.Category = product.Category;
        this.Price = product.Price;
        this.IsActive = product.IsActive;

        if (this.UpdatedAt != default)
            this.UpdatedAt=DateTimeOffset.UtcNow;
        
        this.UpdatedAt = DateTimeOffset.UtcNow;
    }
}
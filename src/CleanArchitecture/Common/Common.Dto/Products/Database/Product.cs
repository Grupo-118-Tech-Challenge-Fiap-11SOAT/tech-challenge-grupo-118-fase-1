using Common.Dto.Base.Database;
using Common.Dto.Order.Database;
using Common.Enums;

namespace Common.Dto.Products.Database;

public class Product : BaseEntity
{
    protected Product()
    {
    }

    public string Name { get; set; }

    public string Description { get; protected set; }

    public ProductType Category { get; protected set; }

    public decimal Price { get; protected set; }

    public ICollection<OrderItem> OrderItems { get; protected set; }

    public List<ImageProduct> Images { get; protected set; }

    public Product(string name,
        string description,
        ProductType productType,
        decimal price,
        bool isActive,
        int id = 0)
    {
        if (id != 0)
            this.Id = id;

        this.IsActive = isActive;

        this.Name = name;
        this.Description = description;
        this.Category = productType;
        this.Price = price;
    }

    public void UpdateProduct(Product productToUpdate)
    {
        this.Name = productToUpdate.Name;
        this.Description = productToUpdate.Description;
        this.Category = productToUpdate.Category;
        this.Price = productToUpdate.Price;
        this.IsActive = productToUpdate.IsActive;
        this.UpdatedAt = DateTimeOffset.Now;
    }
}
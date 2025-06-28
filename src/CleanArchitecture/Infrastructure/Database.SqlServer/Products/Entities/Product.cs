using TechChallengeFastFood.CleanArch.Infrastructure.Database.Base;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Order;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Entities;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Entities;

public class Product : BaseEntity
{
    protected Product()
    {
    }

    public string Name { get; set; }

    public string Description { get; protected set; }

    public string Category { get; protected set; }

    public decimal Price { get; protected set; }

    public ICollection<OrderItem> OrderItems { get; protected set; }

    public List<ImageProduct> Images { get; protected set; }

    public Product(string name,
        string description,
        string productType,
        decimal price,
        bool isActive,
        int id = 0)
    {
        if (id != 0)
            this.Id = id;

        this.Name = name;
        this.Description = description;
        this.Category = productType;
        this.IsActive = isActive;
        this.Price = price;
    }
}
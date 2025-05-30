using Domain.Order.Entities;
using Domain.Products.Exceptions;
using Domain.Products.ValueObjects;

namespace Domain.Products.Entities;

public class Product : Domain.Base.Entities.BaseEntity
{
    private const int MAX_IMAGES = 5;

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
        int id = 0,
        List<ImageProduct>? images = null)
    {
        if (id != 0)
            this.Id = id;

        this.Name = name;
        this.Description = description;
        this.Category = productType;
        this.IsActive = isActive;
        this.Price = price;

        CheckProductValue();

        Images = images ?? new List<ImageProduct>();
    }

    protected Product()
    {
        
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

    public void AddImage(ImageProduct image)
    {
        if (this.Images.Count == MAX_IMAGES)
            throw new ProductMaxImageException();

        this.Images.Add(image);
    }

    public void ChangeImage(ImageProduct image)
    {
        var index = this.Images.FindIndex(i => i.Id == image.Id);

        if (index == -1)
            return;

        this.Images[index] = image;
    }

    private void CheckProductValue()
    {
        if (this.Price <= decimal.Zero)
            throw new ProductInvalidPriceException();
    }
}
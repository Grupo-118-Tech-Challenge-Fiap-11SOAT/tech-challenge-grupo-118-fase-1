using Domain.Products.Dtos;
using Domain.Products.ValueObjects;

namespace Domain.Products.Entities;

public class Product
{
    public int Id { get; private set; }

    public string Name { get; set; }

    public string Description { get; private set; }

    public ProductType Category { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    public List<ImageProduct>? Images { get; private set; }

    public Product(string name, string description, ProductType productType, decimal price, bool isActive)
    {
        this.Name = name;
        this.Description = description;
        this.Category = productType;
        this.IsActive = isActive;

        CheckProductValue(price);

        Images = new List<ImageProduct>();
    }

    public Product(ProductDto productDto)
    {
        if (productDto.Id != 0)
            this.Id = productDto.Id;

        this.Name = productDto.Name;
        this.Description = productDto.Description;
        this.Category = productDto.Category;
        this.IsActive = productDto.IsActive;
        
        CheckProductValue(productDto.Price);

        Images = new List<ImageProduct>();
    }

    public void AddImage(ImageProduct image)
    {
        if (this.Images.Any(im => im.Id.Equals(image.Id)))
            this.Images.Add(image);
    }

    private void CheckProductValue(decimal value)
    {
        if (value <= decimal.Zero)
            throw new ArgumentException($"{nameof(Product)}.{nameof(Product.Price)} cannot be zero or negative.");

        this.Price = value;
    }
}
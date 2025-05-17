using Domain.Products.Exceptions;
using Domain.Products.ValueObjects;

namespace Domain.Products.Entities;

public class Product : BaseDomain
{
    private const int MAX_IMAGES = 5;

    public int Id { get; private set; }

    public string Name { get; set; }

    public string Description { get; private set; }

    public ProductType Category { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    public List<ImageProduct>? Images { get; private set; }

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

    public void AddImage(ImageProduct image)
    {
        if (this.Images.Count == MAX_IMAGES)
            throw new ProductsException("The product already has the maximum number of images.");

        if (this.Images.Any(im => im.Url.Equals(image.Url)))
            throw new ProductsException("The product already has this image.");
        
        this.Images.Add(image);
    }

    public void ChangeImage(ImageProduct image)
    {
        if (this.Images.Any(i => i.Url.Equals(image.Url)))
            throw new ProductsException("The product already has this image.");

        var index = this.Images.FindIndex(i => i.Id == image.Id);
        this.Images[index] = image;
    }

    private void CheckProductValue()
    {
        if (this.Price <= decimal.Zero)
            throw new ProductsException($"{nameof(Product)}.{nameof(Product.Price)} cannot be zero or negative.");
    }
}
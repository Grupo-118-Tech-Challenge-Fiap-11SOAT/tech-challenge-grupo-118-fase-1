using System.Text.Json.Serialization;
using Domain.Products.Entities;
using Domain.Products.ValueObjects;

namespace Domain.Products.Dtos;

public class ProductDto
{
    /// <summary>
    /// The id of the product
    /// </summary>
    public int? Id { get; private set; }

    /// <summary>
    /// The name of the product
    /// </summary>

    public string Name { get; set; }

    /// <summary>
    /// The description of the product
    /// </summary>
    public string Description { get; set; }


    /// <summary>
    /// The category of the product, represented by the ProductType enumeration.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductType Category { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The state of the product.
    /// </summary>
    public bool IsActive { get; set; }


    public ProductDto(Product product)
    {
        this.Id = product.Id;
        this.Name = product.Name;
        this.Description = product.Description;
        this.Category = product.Category;
        this.Price = product.Price;
        this.IsActive = product.IsActive;
    }

    [JsonConstructor]
    public ProductDto(string name, string description, ProductType category, decimal price, bool isActive)
    {
        this.Name = name;
        this.Description = description;
        this.Category = category;
        this.Price = price;
        this.IsActive = isActive;
    }
}
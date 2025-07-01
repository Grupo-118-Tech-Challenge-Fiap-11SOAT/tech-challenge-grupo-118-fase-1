namespace Common.Dto.Products;

public class ProductPersistence
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ProductType Category { get; set; }

    public decimal Price { get; set; }
}
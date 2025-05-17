using Domain;

namespace Infra.Database.SqlServer.Products.Entities;

public class ImageProduct : BaseEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Url { get; set; }

    public int Position { get; set; }

    public Product Product { get; set; }

    public override Domain.Products.Entities.ImageProduct ToDomain()
    {
        return new Domain.Products.Entities.ImageProduct(this.ProductId, this.Position, this.Url);
    }

    public override void DomainToEntity(BaseDomain domain)
    {
        var imageProduct = (Domain.Products.Entities.ImageProduct)domain;

        this.ProductId = imageProduct.ProductId;
        this.Position = imageProduct.Position;
        this.Url = imageProduct.Url;

        if (this.UpdatedAt != default)
            this.UpdatedAt = DateTimeOffset.UtcNow;
    }
}
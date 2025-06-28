using TechChallengeFastFood.CleanArch.Infrastructure.Database.Base;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Entities;

public class ImageProduct : BaseEntity
{
    public int ProductId { get; protected set; }

    public int Position { get; protected set; }

    public string Url { get; protected set; }

    public Product Product { get; protected set; }

    public ImageProduct(int productId, int position, string url, int id = 0)
    {
        if (id != 0)
            this.Id = id;

        this.ProductId = productId;
        this.Position = position;
        this.Url = url;
    }
}
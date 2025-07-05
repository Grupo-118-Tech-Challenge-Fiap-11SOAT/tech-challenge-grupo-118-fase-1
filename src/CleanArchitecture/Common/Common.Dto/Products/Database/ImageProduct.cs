using Common.Dto.Base.Database;

namespace Common.Dto.Products.Database;

public class ImageProduct : BaseEntity
{
    protected ImageProduct()
    {
    }

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
    
    public void UpdateImageProduct(ImageProduct imageToUpdate)
    {
        this.Position = imageToUpdate.Position;
        this.Url = imageToUpdate.Url;
        this.UpdatedAt = DateTimeOffset.Now;
    }
}
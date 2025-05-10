namespace Domain.Products.Entities;

public class ImageProduct
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public string Url { get; set; }

    public ImageProduct(int productId, string url)
    {
        this.ProductId = productId;
        this.Url = url;
    }
}
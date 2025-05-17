using System.Text.RegularExpressions;
using Domain.Products.Exceptions;

namespace Domain.Products.Entities;

public class ImageProduct
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Position { get; set; }

    public string Url { get; set; }

    private readonly Regex _imageRegex = new Regex(@"(\W)(jpg|jpeg|png|gif|webp)", RegexOptions.Compiled);

    public ImageProduct(int productId, int position, string url)
    {
        this.ProductId = productId;
        this.Position = position;
        this.Url = url;

        CheckImageUrlFormat();
        CheckIfIsAValidPosition();
    }

    private ImageProduct()
    {
    }

    public static ImageProduct Load(int id, string url, int productId, int position)
    {
        return new ImageProduct
        {
            Id = id,
            ProductId = productId,
            Position = position,
            Url = url
        };
    }

    private void CheckImageUrlFormat()
    {
        if (!Uri.IsWellFormedUriString(this.Url, UriKind.Absolute))
            throw new ImageProductsException("The provided URL is not valid ");

        if (!_imageRegex.IsMatch(this.Url))
            throw new ImageProductsException("The provided URL is not valid image");
    }

    private void CheckIfIsAValidPosition()
    {
        if (this.Position <= 0)
            throw new ImageProductsException("The provided position should be greater than zero");
    }
}
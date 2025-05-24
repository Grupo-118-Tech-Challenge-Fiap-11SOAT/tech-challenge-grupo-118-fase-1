using System.Text.RegularExpressions;
using Domain.Products.Exceptions;

namespace Domain.Products.Entities;

public class ImageProduct : BaseDomain
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Position { get; set; }

    public string Url { get; set; }

    private readonly Regex _imageRegex = new Regex(@"(\W)(jpg|jpeg|png|gif|webp)", RegexOptions.Compiled);

    public ImageProduct(int productId, int position, string url, int id = 0)
    {
        if (id != 0)
            this.Id = id;

        this.ProductId = productId;
        this.Position = position;
        this.Url = url;

        CheckImageUrlFormat();
        CheckIfIsAValidPosition();
    }

    private void CheckImageUrlFormat()
    {
        if (!Uri.IsWellFormedUriString(this.Url, UriKind.Absolute))
            throw new UrlNotValidException();

        if (!_imageRegex.IsMatch(this.Url))
            throw new UrlIsNotAnImageException();
    }

    private void CheckIfIsAValidPosition()
    {
        if (this.Position <= 0)
            throw new ImagePositionException();
    }
}
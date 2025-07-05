using System.Text.RegularExpressions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Entities;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

public class ImageProduct : BaseEntity
{
    public int ProductId { get; protected set; }

    public int Position { get; protected set; }

    public string Url { get; protected set; }

    public Product Product { get; protected set; }

    private readonly Regex _imageRegex = new Regex(@"(\W)(jpg|jpeg|png|gif|webp)", RegexOptions.Compiled);

    public ImageProduct(int productId,
        int position,
        string url,
        int id = 0,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null) : base(id, createdAt, updatedAt)
    {
        this.ProductId = productId;
        this.Position = position;
        this.Url = url;

        CheckImageUrlFormat();
        CheckIfIsAValidPosition();
    }

    protected ImageProduct()
    {
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
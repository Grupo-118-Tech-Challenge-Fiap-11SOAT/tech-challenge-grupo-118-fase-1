namespace Infra.Database.SqlServer.Products.Entities;

public class ImageProduct : BaseEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Url { get; set; }

    public int Position { get; set; }

    public Product Product { get; set; }
}
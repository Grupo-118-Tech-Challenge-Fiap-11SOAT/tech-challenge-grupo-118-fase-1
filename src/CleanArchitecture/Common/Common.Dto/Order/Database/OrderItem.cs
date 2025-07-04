using Common.Dto.Products.Database;

namespace Common.Dto.Order.Database;

public class OrderItem
{
    protected OrderItem()
    {
    }

    public int ProductId { get; protected set; }
    public int OrderId { get; protected set; }
    public int Quantity { get; protected set; }
    public Common.Dto.Order.Database.Order Order { get; protected set; }
    public Product Product { get; protected set; }
}
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Entities;

public class OrderItem
{
    protected OrderItem()
    {
    }

    public int ProductId { get; protected set; }
    public int OrderId { get; protected set; }
    public int Quantity { get; protected set; }
    public Order Order { get; protected set; }
    public Product Product { get; protected set; }
}
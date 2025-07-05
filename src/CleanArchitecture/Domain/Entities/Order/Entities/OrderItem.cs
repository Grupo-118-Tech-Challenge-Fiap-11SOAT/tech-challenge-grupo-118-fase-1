using TechChallengeFastFood.CleanArch.Domain.Entities.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;

public class OrderItem
{
    public int ProductId { get; protected set; }
    public int OrderId { get; protected set; }
    public int Quantity { get; protected set; }
    public Order Order { get; protected set; }
    public Product Product { get; protected set; }
    
    public OrderItem(int productId, int quantity, int orderId = 0)
    {
        this.ProductId = productId;
        this.Quantity = quantity;

        if (orderId != 0)
            this.OrderId = orderId;
    }
}
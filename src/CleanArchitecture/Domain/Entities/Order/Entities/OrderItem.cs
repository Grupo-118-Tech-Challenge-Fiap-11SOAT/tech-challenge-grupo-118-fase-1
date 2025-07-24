using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;

public class OrderItem
{
    public int ProductId { get; protected set; }
    public int OrderId { get; protected set; }
    public int Quantity { get; protected set; }
    public Order Order { get; protected set; }
    public Product Product { get; protected set; }

    public decimal TotalValue => Product.Price * Quantity;
    public decimal UnitPrice => Product.Price; //TODO: unit price should be persisted instead of calculated

    public OrderItem(int productId, int quantity, int orderId = 0)
    {
        this.ProductId = productId;
        this.Quantity = quantity;
        this.OrderId = orderId;
    }
    
    public OrderItem(int productId, int quantity, int orderId, Product product)
    {
        this.ProductId = productId;
        this.Quantity = quantity;
        this.OrderId = orderId;
        this.Product = product;
    }
}
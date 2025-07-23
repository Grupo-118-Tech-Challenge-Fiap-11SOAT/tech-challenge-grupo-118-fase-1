using Domain.Products.Entities;

namespace Domain.Order.Entities;

public class OrderItem
{
    public int ProductId { get; protected set; }
    public int OrderId { get; protected set; }
    public int Quantity { get; protected set; }
    public Order Order { get; protected set; }
    public Product Product { get; protected set; }

    public decimal TotalValue => Product.Price * Quantity;
    public decimal UnitPrice => Product.Price; //TODO: unit price should be persisted instead of calculated

    public OrderItem(int orderId, int productId, int quantity)
    {
        this.ProductId = productId;
        this.OrderId = orderId;
        this.Quantity = quantity;
    }
}
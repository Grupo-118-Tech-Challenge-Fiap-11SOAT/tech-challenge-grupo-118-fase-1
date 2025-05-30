using Domain.Products.Entities;

namespace Domain.Order.Entities;

public class OrderItem
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
}
using Common.Dto.Base.Database;

namespace Common.Dto.Order.Database;

public class Order : BaseEntity
{
    protected Order()
    {
    }

    public int OrderNumber { get; protected set; }
    public string? Cpf { get; protected set; }
    public decimal Total { get; protected set; }
    public OrderStatus Status { get; protected set; }
    public ICollection<OrderItem> OrderItems { get; protected set; }
}
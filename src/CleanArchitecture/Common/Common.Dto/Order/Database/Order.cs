using Common.Dto.Base.Database;
using Common.Dto.Payments.Database;
using Common.Enums;

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

    public Payment Payment { get; protected set; }
    
    public Order(int orderNumber, string? cpf, decimal total, OrderStatus status, bool isActive,
        List<OrderItem> orderItems = null, int id = 0)
    {
        if (id != 0)
            this.Id = id;

        this.IsActive = isActive;

        this.OrderNumber = orderNumber;
        this.Cpf = cpf;
        this.Total = total;
        this.Status = status;
        this.OrderItems = orderItems is null ? new List<OrderItem>() : orderItems;
    }
}
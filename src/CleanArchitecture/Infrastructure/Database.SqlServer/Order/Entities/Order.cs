using Common.Dto.Order;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Base;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Entities;

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
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Base;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Entities;

public class Order : BaseEntity
{
    public int OrderNumber { get; protected set; }
    public string? Cpf { get; protected set; }
    public decimal Total { get; protected set; }
    public string Status { get; protected set; }
    public ICollection<OrderItem> OrderItems { get; protected set; }
}
using Common.Enums;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Entities;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Extensions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Order.Exceptions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities;

public class Order : BaseEntity
{
    private readonly Random _random = new();

    public int OrderNumber { get; protected set; }
    public string? Cpf { get; protected set; }
    public decimal Total { get; protected set; }
    public OrderStatus Status { get; protected set; }
    public ICollection<OrderItem> OrderItems { get; protected set; }

    private static readonly Dictionary<OrderStatus, OrderStatus?> NextStatus = new()
    {
        { OrderStatus.Received, OrderStatus.InPreparation },
        { OrderStatus.InPreparation, OrderStatus.Ready },
        { OrderStatus.Ready, OrderStatus.Completed },
        { OrderStatus.Completed, null },
        { OrderStatus.Canceled, null }
    };

    public Order(int orderNumber,
        string? cpf,
        decimal total,
        OrderStatus status,
        bool isActive,
        List<OrderItem>? orderItems,
        int id = 0,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null)
    {
        if (id != 0)
            this.Id = id;

        this.OrderNumber = orderNumber;

        this.Cpf = cpf;
        this.Total = total;
        this.Status = status;
        this.IsActive = isActive;

        this.OrderItems = orderItems ?? new List<OrderItem>();

        if (createdAt is not null)
            this.CreatedAt = createdAt.Value;

        if (updatedAt is not null)
            this.UpdatedAt = updatedAt.Value;
    }

    public Order(string? cpf, List<OrderItem> orderItems, List<Product>? products)
    {
        this.OrderNumber = _random.Next(100000, 1000000);

        this.Cpf = cpf is null ? cpf : cpf.SanitizeCpf();

        this.Status = OrderStatus.Received;
        this.CreatedAt = DateTimeOffset.Now;
        this.UpdatedAt = DateTimeOffset.Now;

        this.OrderItems = orderItems?
                              .Select(item => new OrderItem(item.ProductId, item.Quantity, item.OrderId)).ToList() ??
                          new List<OrderItem>();

        this.Total = orderItems?.Sum(item =>
        {
            var product = products?.FirstOrDefault(p => p.Id == item.ProductId);
            return product?.Price * item.Quantity ?? 0;
        }) ?? 0;

        ValidateOrder();
    }

    private void ValidateOrder()
    {
        if (Cpf is not null && !Cpf.IsValidCpf())
            throw new InvalidCpfException();

        if (OrderItems.Count == 0)
            throw new EmptyOrderItemsException();
    }

    public void ChangeStatus()
    {
        if (NextStatus.TryGetValue(Status, out OrderStatus? nextStatus))
        {
            if (nextStatus is null)
                throw new ChangeStatusNotAllowed(Status);

            Status = nextStatus.Value;
        }
        else
        {
            throw new ChangeStatusInvalidException();
        }
    }
}
using Domain.Order.Dtos;
using Domain.Base.Entities;
using Domain.Order.Exceptions;
using Domain.Products.Ports.In;
using System.Threading;
using Domain.Products.Dtos;

namespace Domain.Order.Entities;

public class Order : BaseEntity
{
    public int OrderNumber { get; set; }
    public string Cpf { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }


    private static readonly Dictionary<OrderStatus, OrderStatus?> NextStatus = new()
    {
        { OrderStatus.Recebido, OrderStatus.EmPreparacao },
        { OrderStatus.EmPreparacao, OrderStatus.Pronto },
        { OrderStatus.Pronto, OrderStatus.Finalizado },
        { OrderStatus.Finalizado, null },
        { OrderStatus.Cancelado, null }
    };

    public Order()
    {
    }


    public Order(OrderDto orderDto, List<ProductDto> products)
    {
        var random = new Random();

        OrderNumber = random.Next(100000, 1000000);
        Cpf = orderDto.Cpf;
        Status = OrderStatus.Recebido;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;

        OrderItems = orderDto.Items.Select(item => new OrderItem
        {
            OrderId = orderDto.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity
        }).ToList() ?? new List<OrderItem>();

        Total = orderDto.Items.Sum(item =>
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            return product.Price * item.Quantity;
        });

    }

    public void ChangeStatus()
    {
        if (NextStatus.TryGetValue(Status, out OrderStatus? nextStatus))
        {
            if (nextStatus is null)           
                throw new ChangeStatusNotAllowed(Status);
            

            Status = nextStatus.Value;
            UpdatedAt = DateTimeOffset.Now;
        }
        else
        {
            throw new ChangeStatusInvalidException();
        }
    }
}
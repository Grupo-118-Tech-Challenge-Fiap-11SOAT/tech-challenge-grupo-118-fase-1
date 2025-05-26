using Domain.Employee.Exceptions;
using Domain.Order.Dtos;
using System;
using Domain.Base.Entities;

namespace Domain.Order.Entities;

public class Order : BaseEntity
{
    public int OrderNumber { get; set; }
    public string Cpf { get; set; }
    public double Total { get; set; }
    public OrderStatus Status { get; set; }

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

    public Order(OrderDto orderDto)
    {
        var random = new Random();

        OrderNumber = random.Next(100000, 1000000);
        Cpf = orderDto.Cpf;
        Total = orderDto.Total;
        Status = OrderStatus.Recebido;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public void ChangeStatus()
    {
        if (NextStatus.TryGetValue(Status, out OrderStatus? nextStatus))
        {
            if (nextStatus is null)
            {
                throw new InvalidOperationException(
                    $"Não é possível alterar o status quando ele está como '{Status}'.");
            }

            Status = nextStatus.Value;
            UpdatedAt = DateTimeOffset.Now;
        }
        else
        {
            throw new InvalidOperationException($"Status atual '{Status}' não é reconhecido.");
        }
    }
}
using Domain.Employee.Dtos;
using Domain.Order.Entities;
using System;

namespace Domain.Order.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Cpf { get; set; }
    public double Total { get; set; }
    public OrderStatus Status { get; set; }

    public OrderDto()
    {
        
    }
    public OrderDto(Entities.Order order)
    {
        Id = order.Id;
        OrderNumber = order.OrderNumber;
        Cpf = order.Cpf;
        Total = order.Total;
        Status = order.Status;
    }

    public static OrderDto ToDto(Entities.Order order)
    {
        return new OrderDto
        {
            Cpf = order.Cpf,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            Total = order.Total
        };
    }

    public static Entities.Order ToEntity(OrderDto orderDto)
    {
        var random = new Random();

        return new Entities.Order
        {
            Cpf = orderDto.Cpf,
            OrderNumber = random.Next(100000, 1000000),
            Total = orderDto.Total,
            Status = OrderStatus.Recebido,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}
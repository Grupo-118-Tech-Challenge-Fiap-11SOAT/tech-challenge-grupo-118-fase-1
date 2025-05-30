using Domain.Order.Entities;
using System.Text.Json.Serialization;


namespace Domain.Order.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string? Cpf { get; set; }
    public decimal Total { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; }
    public List<OrderItemDto> Items { get; set; }

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
        Items = order.OrderItems?.Select(item => new OrderItemDto
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity
        }).ToList() ?? new List<OrderItemDto>();
    }
}
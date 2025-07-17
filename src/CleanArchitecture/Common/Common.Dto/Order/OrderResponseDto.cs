using System.Text.Json.Serialization;
using Common.Enums;

namespace Common.Dto.Order;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int OrderNumber { get; set; }

    public string? Cpf { get; set; }

    public decimal Total { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? CreatedAt { get; set; }

    public List<OrderItemDto> Items { get; set; }

    public OrderResponseDto(int id, int orderNumber, string? cpf, decimal total, OrderStatus status,
        List<OrderItemDto> items, DateTimeOffset? createdAt)
    {
        Id = id;
        OrderNumber = orderNumber;
        Cpf = cpf;
        Total = total;
        Status = status;
        CreatedAt = createdAt;
        Items = items ?? new List<OrderItemDto>();
    }
}
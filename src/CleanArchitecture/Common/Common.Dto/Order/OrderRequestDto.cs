namespace Common.Dto.Order;

public class OrderRequestDto
{
    public string? Cpf { get; set; }
    
    public List<OrderItemDto> Items { get; set; }
}
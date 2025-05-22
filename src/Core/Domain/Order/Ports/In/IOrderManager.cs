using Domain.Order.Dtos;
using Domain.Order.Entities;

namespace Domain.Order.Ports.In;

public interface IOrderManager
{
    Task<List<OrderDto?>> GetAllAsync(OrderStatus status, int skip = 0, int take = 10, CancellationToken cancellationToken = default);

    Task<OrderDto> CreateAsync(OrderDto orderDto, CancellationToken cancellationToken);

    Task<OrderDto> UpdateStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken);
}
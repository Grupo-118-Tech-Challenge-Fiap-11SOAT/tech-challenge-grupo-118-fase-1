using Domain.Order.Dtos;
using Domain.Order.Entities;

namespace Domain.Order.Ports.Out;

public interface IOrderRepository
{
    Task<List<Entities.Order>> GetAllAsync(OrderStatus status, CancellationToken cancellationToken = default, int skip = 0, int take = 10);

    Task<Entities.Order> CreateAsync(Entities.Order order, CancellationToken cancellationToken);

    Task<Entities.Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Entities.Order> UpdateAsync(Entities.Order order, CancellationToken cancellationToken = default);
}
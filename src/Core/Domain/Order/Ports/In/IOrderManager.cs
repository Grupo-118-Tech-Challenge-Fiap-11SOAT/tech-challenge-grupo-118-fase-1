using Domain.Products.Dtos;

namespace Domain.Order.Ports.In;

public interface IOrderManager
{
    Task<List<OrderDto>> GetOrdersAsync(int skip = 0, int take = 10);
}
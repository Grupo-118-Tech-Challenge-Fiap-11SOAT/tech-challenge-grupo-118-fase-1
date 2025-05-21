using Domain.Products.Entities;

namespace Domain.Order.Ports.Out;

public interface IOrderRepository
{
    Task<List<Product>> GetOrdersAsync(int skip = 0, int take = 10);
}
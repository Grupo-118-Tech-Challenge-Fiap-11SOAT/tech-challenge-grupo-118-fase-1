using Domain.Order.Entities;
using Domain.Order.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Order.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Domain.Order.Entities.Order>> GetAllAsync(OrderStatus status,
        CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var orders = await _dbContext.Orders
            .Where(o => o.Status.Equals(status))
            .Skip(skip)
            .Take(take)
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<Domain.Order.Entities.Order> CreateAsync(Domain.Order.Entities.Order order,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Domain.Order.Entities.Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Domain.Order.Entities.Order> UpdateAsync(Domain.Order.Entities.Order order,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order;
    }
}
using Common.Enums;
using Common.Interfaces.Order.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly CleanArchDbContext _dbContext;

    public OrderRepository(CleanArchDbContext context)
    {
        _dbContext = context;
    }

    public async Task<List<Common.Dto.Order.Database.Order>> GetAllAsync(OrderStatus status,
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

    public async Task<List<Common.Dto.Order.Database.Order>> GetOrdersToMonitorAsync(
        CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var orders = await _dbContext.Orders
            .Where(o => o.Status != OrderStatus.Completed && o.Status != OrderStatus.Canceled)
            .Skip(skip)
            .Take(take)
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.Status == OrderStatus.Ready)
            .ThenByDescending(o => o.Status == OrderStatus.InPreparation)
            .ThenByDescending(o => o.Status == OrderStatus.Received)
            .ThenBy(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<Common.Dto.Order.Database.Order> CreateAsync(Common.Dto.Order.Database.Order order,
        CancellationToken cancellationToken)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order;
    }

    public async Task<Common.Dto.Order.Database.Order?> GetByIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Common.Dto.Order.Database.Order?> GetByIdWithPaymentAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Include(x => x.OrderItems)
            .Include(x => x.Payment)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<Common.Dto.Order.Database.Order> UpdateAsync(Common.Dto.Order.Database.Order order,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Update(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order;
    }
}
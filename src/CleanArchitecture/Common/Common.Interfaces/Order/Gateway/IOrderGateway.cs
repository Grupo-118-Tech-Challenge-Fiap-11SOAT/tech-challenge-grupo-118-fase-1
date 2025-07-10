using OrderDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order;
using OrderEntity = Common.Dto.Order.Database.Order;

namespace Common.Interfaces.Order.Gateway;

public interface IOrderGateway
{
    /// <summary>
    /// Retrieves a list of orders with optional filtering by status and pagination.
    /// </summary>
    /// <param name="status">The status of the orders to filter by.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <param name="skip">The number of items to skip (for pagination).</param>
    /// <param name="take">The number of items to take (for pagination).</param>
    /// <returns>A list of orders matching the specified criteria, or null if no orders are found.</returns>
    Task<List<OrderDomain>?> GetAllAsync(
        Common.Enums.OrderStatus status,
        CancellationToken cancellationToken = default, int skip = 0, int take = 10);

    /// <summary>
    /// Retrieves a list of orders that will be displayed in monitoring displays, based on specific criteria.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    Task<List<OrderDomain>?> GetOrdersToMonitorAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10);    
    
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order entity to create.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The created order entity.</returns>
    Task<OrderDomain> CreateAsync(OrderDomain order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The order matching the specified ID.</returns>
    Task<OrderDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order by its ID, including payment details.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<OrderDomain?> GetByIdWithPaymentAsync(int id, CancellationToken cancellationToken = default);    
    
    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="order">The updated order entity.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The updated order entity.</returns>
    Task<OrderDomain> UpdateAsync(OrderDomain order, CancellationToken cancellationToken = default);
}
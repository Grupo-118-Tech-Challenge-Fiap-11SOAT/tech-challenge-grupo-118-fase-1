using Common.Enums;

namespace Common.Interfaces.Order.Repositories;

public interface IOrderRepository
{
    /// <summary>
    /// Retrieves a list of orders based on pagination parameters.
    /// </summary>
    /// <param name="status">Order Status to filter content.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of orders.</returns>
    Task<List<Dto.Order.Database.Order>> GetAllAsync(OrderStatus status,
        CancellationToken cancellationToken = default, int skip = 0, int take = 10);

    /// <summary>
    /// Retrieves a list of orders following a specific criteria for monitoring purposes.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    Task<List<Dto.Order.Database.Order>> GetOrdersToMonitorAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10);
    
    /// <summary>
    /// Creates a new order in the system.
    /// </summary>
    /// <param name="order">The order entity containing the details of the order to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The unique identifier of the created order.</returns>    
    Task<Dto.Order.Database.Order> CreateAsync(Dto.Order.Database.Order order, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a oder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The order matching the specified identifier, or null if no such order exists.</returns>
    Task<Dto.Order.Database.Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order by its unique identifier, including payment details.
    /// </summary>
    /// <param name="id">The unique identifier of the order</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Common.Dto.Order.Database.Order?> GetByIdWithPaymentAsync(int id,
        CancellationToken cancellationToken = default);    
    
    /// <summary>
    /// Updates an existing order with the provided details.
    /// Updates an existing order with the provided details.
    /// </summary>
    /// <param name="order">The order data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated Order entity</returns>
    Task<Dto.Order.Database.Order> UpdateAsync(Dto.Order.Database.Order order,
        CancellationToken cancellationToken = default);
}
using Common.Dto.Order;

namespace Common.Interfaces.Order.Controller;

public interface IOrderController
{
    /// <summary>
    /// Retrieves a list of orders with optional filtering by status and pagination.
    /// </summary>
    /// <param name="status">The status of the orders to filter by.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <param name="skip">The number of items to skip (for pagination).</param>
    /// <param name="take">The number of items to take (for pagination).</param>
    /// <returns>A list of orders matching the specified criteria, or null if no orders are found.</returns>
    Task<List<OrderResponseDto>?> GetAllAsync(
        Common.Enums.OrderStatus status,
        CancellationToken cancellationToken = default, int skip = 0, int take = 10);

    /// <summary>
    /// Retrieves a list of orders that will be displayed in monitoring displays, with optional pagination.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    Task<List<OrderResponseDto>?> GetOrdersToMonitorAsync(CancellationToken cancellationToken = default, int skip = 0,
        int take = 10);

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order entity to create.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The created order entity.</returns>
    Task<OrderResponseDto> CreateAsync(OrderRequestDto order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The order matching the specified ID.</returns>
    Task<OrderResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The updated order entity.</returns>
    Task<OrderResponseDto?> UpdateStatusAsync(int orderId, CancellationToken cancellationToken = default);
}
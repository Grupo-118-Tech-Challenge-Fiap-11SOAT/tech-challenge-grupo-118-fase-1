using Common.Dto.Order;
using Common.Enums;
using Common.Interfaces.Order.Controller;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Handles HTTP requests related to order operations such as creating, retrieving, updating,
/// and managing the status of orders.
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderController _orderController;

    private readonly ProblemDetails ORDER_NOT_FOUND = new ProblemDetails
    {
        Title = "Order not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The specified order was not found."
    };

    public OrderController(IOrderController orderController)
    {
        _orderController = orderController;
    }

    /// <summary>
    /// Retrieves all orders with the specified status and supports pagination.
    /// </summary>
    /// <param name="status">The status of the orders to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for request cancellation.</param>
    /// <param name="skip">The number of records to skip. Used for pagination.</param>
    /// <param name="take">The number of records to take. Used for pagination.</param>
    /// <returns>The list of orders matching the specified criteria or appropriate status code if no orders are found.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(OrderStatus status, CancellationToken cancellationToken, int skip = 0,
        int take = 10)
    {
        var orders = await _orderController.GetAllAsync(status, cancellationToken, skip, take);

        if (orders is null || orders.Count == 0)
            return NotFound(ORDER_NOT_FOUND);

        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order based on the provided order data.
    /// </summary>
    /// <param name="orderDto">The data transfer object containing the details of the order to create.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for request cancellation.</param>
    /// <returns>The details of the created order, including its unique identifier.</returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OrderRequestDto orderDto, CancellationToken cancellationToken)
    {
        var result = await _orderController.CreateAsync(orderDto, cancellationToken);

        return CreatedAtAction("GetById", new { result.Id }, result);
    }

    /// <summary>
    /// Updates the status of an order identified by its ID.
    /// </summary>
    /// <param name="id">The ID of the order whose status needs to be updated.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for request cancellation.</param>
    /// <returns>The result of the operation or an appropriate status code.</returns>
    [HttpPatch("{id}/change-status")]
    public async Task<IActionResult> PatchStatus(int id, CancellationToken cancellationToken)
    {
        var result = await _orderController.UpdateStatusAsync(id, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for request cancellation.</param>
    /// <returns>The order details if found, or a NotFound status if the order does not exist.</returns>
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}"), ActionName("GetById")]
    public async Task<ActionResult<OrderResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _orderController.GetByIdAsync(id, cancellationToken);

        return order is null ? NotFound(ORDER_NOT_FOUND) : Ok(order);
    }
}
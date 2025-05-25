using Domain.Order.Dtos;
using Domain.Order.Entities;
using Domain.Order.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

/// <summary>
/// Controller to Manage Products
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderManager _orderManager;

    public OrderController(IOrderManager orderManager)
    {
        _orderManager = orderManager;
    }
    
    /// <summary>
    /// List Products
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(OrderStatus status, CancellationToken cancellationToken, int skip = 0, int take = 0)
    {
        var orders =
            await _orderManager.GetAllAsync(status, skip, take, cancellationToken);

        if (orders is null || orders.Count == 0)
            return NotFound();

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OrderDto orderDto, CancellationToken cancellationToken)
    {
        var result = await _orderManager.CreateAsync(orderDto, cancellationToken);

        return CreatedAtAction("GetById", new { result.Id }, result);
    }

    [HttpPatch("{id}/change-status")]
    public async Task<IActionResult> PatchStatus(int id, CancellationToken cancellationToken)
    {

        var result = await _orderManager.UpdateStatusAsync(id, cancellationToken);

        return Ok(result);
    }

    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}"), ActionName("GetById")]
    public async Task<ActionResult<OrderDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var order = await _orderManager.GetByIdAsync(id, cancellationToken);

        return order is null ? NotFound() : Ok(order);
    }
}
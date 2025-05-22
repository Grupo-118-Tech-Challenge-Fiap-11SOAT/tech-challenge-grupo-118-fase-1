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

    [HttpPatch("{id}/status/{status}")]
    public async Task<IActionResult> PatchStatus(int id, OrderStatus status, CancellationToken cancellationToken)
    {

        var result = await _orderManager.UpdateStatusAsync(id, status, cancellationToken);

        return Ok(result);
    }
}
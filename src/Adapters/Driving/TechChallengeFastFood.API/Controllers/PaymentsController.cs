using Domain.Payments.Dtos;
using Domain.Payments.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

/// <summary>
/// Payments controller
/// </summary>
[ApiController]
[Route("payments")]
public class PaymentsController(IPaymentManager paymentManager) : ControllerBase
{
    /// <summary>
    /// Creates a payment.
    /// </summary>
    /// <param name="request">The requested payment informations</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<PaymentResponse>> CreatePaymentAsync([FromBody] PaymentRequest request, CancellationToken cancellationToken)
    {
        var response = await paymentManager.CreatePaymentAsync(request, cancellationToken);
        return Created(response.Id.ToString(), response);
    }

    /// <summary>
    /// Simulates a payment confirmation
    /// </summary>
    /// <param name="id">The updated payment id</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentResponse>> ConfirmPaymentAsync(int id, CancellationToken cancellationToken)
    {
        var response = await paymentManager.ConfirmPaymentAsync(id, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Precess a payment update from Mercado Pago webhook.
    /// </summary>
    /// <param name="request">The updated payment data</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaymentCallbackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("webhooks/mercado-pago")]
    public async Task<IActionResult> ProcessCallbackAsync([FromBody] PaymentCallbackRequest request, CancellationToken cancellationToken)
    {
        await paymentManager.ProcessCallbackAsync(request, cancellationToken);
        return Ok();
    }
}
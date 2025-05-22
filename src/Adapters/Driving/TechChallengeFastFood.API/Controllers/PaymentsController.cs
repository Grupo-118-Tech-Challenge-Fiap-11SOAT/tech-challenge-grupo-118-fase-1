using Domain;
using Domain.Payments.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentManager _paymentManager;

    public PaymentsController(IPaymentManager paymentManager)
    {
        _paymentManager = paymentManager;
    }

    /// <summary>
    /// Creates a payment.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        await _paymentManager.CreatePaymentAsync(cancellationToken);
        return Ok();
    }
}
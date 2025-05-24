using Domain.Payments.Entities;

namespace Domain.Payments.Ports.Out;

public interface IPaymentProcessor
{
    /// <summary>
    /// Processes the payment in the payment provider.
    /// </summary>
    /// <param name="payment">The payment to process.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task ProcessAsync(Payment payment, CancellationToken cancellationToken = default);
}
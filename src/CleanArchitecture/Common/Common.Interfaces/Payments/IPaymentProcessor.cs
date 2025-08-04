using Common.Dto.Payments;
using OrderDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Order.Entities.Order;

namespace Common.Interfaces.Payments;

public interface IPaymentProcessor
{
    /// <summary>
    /// Processes the payment in the payment provider.
    /// </summary>
    /// <param name="payment">The payment to process.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<ProcessedPaymentDto> ProcessAsync(PaymentExternalDto payment, OrderDomain order, CancellationToken cancellationToken = default);
}
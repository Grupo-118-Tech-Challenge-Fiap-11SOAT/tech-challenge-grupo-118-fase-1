using Domain.Payments.Dtos;

namespace Domain.Payments.Ports.In;

public interface IPaymentManager
{
    /// <summary>
    /// Creates a new payment
    /// </summary>
    /// <param name="paymentDto">The requested payment informations</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns the generated payment.</returns>
    Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default);
}
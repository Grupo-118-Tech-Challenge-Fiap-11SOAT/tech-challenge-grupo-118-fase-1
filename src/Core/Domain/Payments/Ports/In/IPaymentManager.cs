using Domain.Payments.Dtos;

namespace Domain.Payments.Ports.In;

public interface IPaymentManager
{
    /// <summary>
    /// Creates a new payment
    /// </summary>
    /// <param name="request">The requested payment informations</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns the generated payment.</returns>
    Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Simulates a payment confirmation
    /// </summary>
    /// <param name="id">The payment id</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns the updated payment.</returns>
    Task<PaymentResponse> ConfirmPaymentAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Process a payment update from Mercado Pago webhook
    /// </summary>
    /// <param name="request">The callback content</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns the generated payment.</returns>
    Task<PaymentCallbackResponse> ProcessCallbackAsync(PaymentCallbackRequest request, CancellationToken cancellationToken = default);
}
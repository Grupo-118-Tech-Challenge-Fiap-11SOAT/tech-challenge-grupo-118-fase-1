using Domain.Products.Entities;

namespace Domain.Payments.Ports.Out;

public interface IPaymentRepository
{
    /// <summary>
    /// Creates a payment
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The unique identifier of the created product.</returns>
    Task CreateQrCodeAsync(CancellationToken cancellationToken = default);
}
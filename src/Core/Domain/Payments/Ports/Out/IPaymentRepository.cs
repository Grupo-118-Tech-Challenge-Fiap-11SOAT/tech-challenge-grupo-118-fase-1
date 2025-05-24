using Domain.Payments.Entities;

namespace Domain.Payments.Ports.Out;

public interface IPaymentRepository
{
    /// <summary>
    /// Creates a new payment record in the database.
    /// </summary>
    /// <param name="payment">The payment that will be persisted</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task CreateAsync(Payment payment, CancellationToken cancellationToken = default);
}
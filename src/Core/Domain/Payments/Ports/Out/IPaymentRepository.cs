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

    /// <summary>
    /// Retrieves a payment by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the payment.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The payment matching the specified identifier, or null if no such order exists.</returns>
    Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing payment record in the database.
    /// </summary>
    /// <param name="payment">The payment that will be updated</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default);
}
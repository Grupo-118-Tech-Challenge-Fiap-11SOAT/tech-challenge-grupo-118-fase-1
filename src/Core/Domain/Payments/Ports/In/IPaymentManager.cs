namespace Domain.Payments.Ports.In;

public interface IPaymentManager
{
    /// <summary>
    /// Creates a new payment
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns the unique identifier of the created product.</returns>
    Task CreatePaymentAsync(CancellationToken cancellationToken = default);
}
using Domain.Payments.Entities;

namespace Domain.Payments.Services.Interfaces;

public interface IPaymentService
{
    /// <summary>
    /// Retrieves a payment by its unique identifier and thorows an exception if not found
    /// </summary>
    /// <param name="ind">The payment id</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The payment</returns>
    Task<Payment> ValidateByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Confirm a payment
    /// </summary>
    /// <param name="payment">The payment to confirm.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task ConfirmAsync(Order.Entities.Order order, Payment payment, CancellationToken cancellationToken = default);
}
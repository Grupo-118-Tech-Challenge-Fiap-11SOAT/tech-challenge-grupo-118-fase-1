using Domain.Payments.Entities;
using Domain.Payments.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Payments.Repositories;

public class PaymentRepository(AppDbContext context) : IPaymentRepository
{
    /// <summary>
    /// Creates a new payment record in the database.
    /// </summary>
    /// <param name="payment">The payment that will be persisted</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
    public async Task CreateAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await context.Payments.AddAsync(payment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}

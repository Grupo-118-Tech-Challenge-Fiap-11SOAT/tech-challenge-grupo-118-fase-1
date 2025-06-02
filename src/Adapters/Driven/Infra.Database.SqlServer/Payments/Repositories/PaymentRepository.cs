using Domain.Payments.Entities;
using Domain.Payments.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Payments.Repositories;

public class PaymentRepository(AppDbContext context) : IPaymentRepository
{
    public async Task CreateAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await context.Payments.AddAsync(payment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Payments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        context.Update(payment);
        await context.SaveChangesAsync(cancellationToken);
    }
}

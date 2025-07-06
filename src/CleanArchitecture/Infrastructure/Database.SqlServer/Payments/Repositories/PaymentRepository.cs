using Common.Dto.Payments.Database;
using Common.Interfaces.Payments.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Payments.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly CleanArchDbContext _dbContext;

    public PaymentRepository(CleanArchDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Payment> CreateAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Payments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return payment;
    }

    public async Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(payment).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
using Common.Dto.Customers.Database;
using Common.Interfaces.Customer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Customers.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CleanArchDbContext _dbContext;

    public CustomerRepository(CleanArchDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public static ICustomerRepository Create(CleanArchDbContext context)
    {
        return new CustomerRepository(context);
    }

    public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Customer?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .FirstOrDefaultAsync(c => c.Cpf == cpf, cancellationToken);
    }
}

using Domain.Customer.Entities;
using Domain.Customer.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Customers.Repositories
{
    public class CustomersRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken) => await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken) => await _dbContext.Customers.FirstOrDefaultAsync(x => x.Cpf == cpf, cancellationToken);

        public async Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }
    }
}

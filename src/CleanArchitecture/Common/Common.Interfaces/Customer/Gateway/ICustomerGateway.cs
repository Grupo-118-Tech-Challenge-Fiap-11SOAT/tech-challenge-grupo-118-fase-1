using CustomerDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer;

namespace Common.Interfaces.Customer.Gateway;

public interface ICustomerGateway
{
    Task<CustomerDomain?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default);
    Task<CustomerDomain> CreateAsync(CustomerDomain customer, CancellationToken cancellationToken = default);
    Task<CustomerDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CustomerDomain?> UpdateAsync(CustomerDomain customer, CancellationToken cancellationToken = default);
}

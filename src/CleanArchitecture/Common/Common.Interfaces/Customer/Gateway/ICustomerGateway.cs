using CustomerDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer;

namespace Common.Interfaces.Customer.Gateway;

public interface ICustomerGateway
{
    Task<CustomerDomain?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default);
    Task<CustomerDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}

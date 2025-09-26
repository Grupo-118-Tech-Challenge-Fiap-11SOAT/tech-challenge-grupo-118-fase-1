namespace Common.Interfaces.Customer.Repositories;

public interface ICustomerRepository
{
    Task<Dto.Customers.Database.Customer?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default);
    Task<Dto.Customers.Database.Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}

using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Repositories;
using CustomerDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Customer;

/// <summary>
/// Customer gateway implementation, responsible for mediating operations between the presentation layer and the customer repository.
/// </summary>
public class CustomerGateway : ICustomerGateway
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="CustomerGateway"/>.
    /// </summary>
    /// <param name="customerRepository">Customer repository.</param>
    public CustomerGateway(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public static ICustomerGateway Create(ICustomerRepository customerRepository)
    {
        return new CustomerGateway(customerRepository);
    }

    /// <summary>
    /// Asynchronously gets a customer by identifier.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>Domain entity of the customer, or null if not found.</returns>
    public async Task<CustomerDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

        if (customer is null)
            return null;

        return new CustomerDomain
        (
            customer.Cpf,
            customer.Name,
            customer.Surname,
            customer.Email,
            customer.BirthDay,
            customer.IsActive
        );
    }

    /// <summary>
    /// Asynchronously gets a customer by CPF.
    /// </summary>
    /// <param name="cpf">Customer's CPF.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>Domain entity of the customer, or null if not found.</returns>
    public async Task<CustomerDomain?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetCustomerByCpf(cpf, cancellationToken);

        if (customer is null)
            return null;

        return new CustomerDomain
        (
            customer.Cpf,
            customer.Name,
            customer.Surname,
            customer.Email,
            customer.BirthDay,
            customer.IsActive
        );
    }
}
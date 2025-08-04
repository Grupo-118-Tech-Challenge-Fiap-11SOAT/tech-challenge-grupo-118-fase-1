using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Repositories;
using CustomerDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer;
using CustomerEntity = Common.Dto.Customers.Database.Customer;

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
    /// Asynchronously creates a new customer.
    /// </summary>
    /// <param name="customer">Domain entity of the customer to be created.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>Domain entity of the created customer.</returns>
    public async Task<CustomerDomain> CreateAsync(CustomerDomain customer,
        CancellationToken cancellationToken = default)
    {
        var customerEntity = new CustomerEntity
        (
            customer.Id,
            customer.Name,
            customer.Cpf,
            customer.Email
        );

        var createdCustomer = await _customerRepository.CreateAsync(customerEntity, cancellationToken);

        return new CustomerDomain
        (
            createdCustomer.Cpf,
            createdCustomer.Name,
            createdCustomer.Surname,
            createdCustomer.Email,
            createdCustomer.BirthDay,
            createdCustomer.IsActive
        );
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

    /// <summary>
    /// Asynchronously updates a customer's data.
    /// </summary>
    /// <param name="customer">Domain entity of the customer with updated data.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>Domain entity of the updated customer.</returns>
    public async Task<CustomerDomain?> UpdateAsync(CustomerDomain customer,
        CancellationToken cancellationToken = default)
    {
        var customerEntity = new CustomerEntity
        (
            customer.Id,
            customer.Name,
            customer.Cpf,
            customer.Email
        );

        await _customerRepository.UpdateAsync(customerEntity, cancellationToken);

        return customer;
    }
}
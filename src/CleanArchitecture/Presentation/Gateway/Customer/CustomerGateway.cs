using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Repositories;
using CustomerDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer;
using CustomerEntity = Common.Dto.Customers.Database.Customer;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Customer;

public class CustomerGateway : ICustomerGateway
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerGateway(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<CustomerDomain> CreateAsync(CustomerDomain customer, CancellationToken cancellationToken = default)
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

    public async Task<CustomerDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

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

    public async Task<CustomerDomain?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetCustomerByCpf(cpf, cancellationToken);

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

    public async Task<CustomerDomain?> UpdateAsync(CustomerDomain customer, CancellationToken cancellationToken = default)
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

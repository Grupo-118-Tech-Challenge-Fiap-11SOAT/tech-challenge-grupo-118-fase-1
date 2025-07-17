using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Repositories;
using CustomerDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer;
using CustomerEntity = Common.Dto.Customers.Database.Customer;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Customer;

/// <summary>
/// Implementação do gateway de cliente, responsável por intermediar operações entre a camada de apresentação e o repositório de clientes.
/// </summary>
public class CustomerGateway : ICustomerGateway
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CustomerGateway"/>.
    /// </summary>
    /// <param name="customerRepository">Repositório de clientes.</param>
    public CustomerGateway(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    /// <summary>
    /// Cria um novo cliente de forma assíncrona.
    /// </summary>
    /// <param name="customer">Entidade de domínio do cliente a ser criado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Entidade de domínio do cliente criado.</returns>
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

    /// <summary>
    /// Obtém um cliente pelo identificador de forma assíncrona.
    /// </summary>
    /// <param name="id">Identificador do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Entidade de domínio do cliente, ou null se não encontrado.</returns>
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

    /// <summary>
    /// Obtém um cliente pelo CPF de forma assíncrona.
    /// </summary>
    /// <param name="cpf">CPF do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Entidade de domínio do cliente, ou null se não encontrado.</returns>
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

    /// <summary>
    /// Atualiza os dados de um cliente de forma assíncrona.
    /// </summary>
    /// <param name="customer">Entidade de domínio do cliente com dados atualizados.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Entidade de domínio do cliente atualizada.</returns>
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

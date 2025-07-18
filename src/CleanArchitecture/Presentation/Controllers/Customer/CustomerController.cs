using Common.Dto.Customers;
using Common.Interfaces.Customer.Controller;
using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Presenter;
using Common.Interfaces.Customer.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Customer;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Customer;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Customer;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Customer;

public class CustomerController : ICustomerController
{
    private readonly GetCustomerByCpfUseCase _getCustomerByCpfUseCase;
    private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;
    private readonly CreateCustomerUseCase _createCustomerUseCase;
    private readonly UpdateCustomerUseCase _updateCustomerUseCase;

    private readonly ICustomerPresenter _customerPresenter;

    public CustomerController(ICustomerRepository customerRepository)
    {
        ICustomerGateway customerGateway = CustomerGateway.Create(customerRepository);

        _getCustomerByCpfUseCase = GetCustomerByCpfUseCase.Create(customerGateway);
        _getCustomerByIdUseCase = GetCustomerByIdUseCase.Create(customerGateway);
        _createCustomerUseCase = CreateCustomerUseCase.Create(customerGateway);
        _updateCustomerUseCase = UpdateCustomerUseCase.Create(customerGateway);

        _customerPresenter = CustomerPresenter.Create();
    }

    /// <summary>
    /// Cria um novo cliente com os dados informados.
    /// </summary>
    /// <param name="customer">Objeto <see cref="CustomerRequestDto"/> contendo os dados do cliente a ser criado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> contendo os dados do cliente criado.
    /// </returns>
    public async Task<CustomerResponseDto> CreateAsync(CustomerRequestDto customer,
        CancellationToken cancellationToken = default)
    {
        var customerDomain = await _createCustomerUseCase.ExecuteAsync(customer, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    /// <summary>
    /// Obtém um cliente pelo CPF informado.
    /// </summary>
    /// <param name="cpf">CPF do cliente a ser consultado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> contendo os dados do cliente encontrado,
    /// ou <c>null</c> caso não exista cliente com o CPF informado.
    /// </returns>
    public async Task<CustomerResponseDto?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _getCustomerByCpfUseCase.ExecuteAsync(cpf, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    /// <summary>
    /// Obtém os dados de um cliente pelo identificador único.
    /// </summary>
    /// <param name="id">Identificador único do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> contendo os dados do cliente encontrado,
    /// ou <c>null</c> caso não exista cliente com o identificador informado.
    /// </returns>
    public async Task<CustomerResponseDto?> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _getCustomerByIdUseCase.ExecuteAsync(id, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="customer">Objeto <see cref="CustomerUpdateDto"/> contendo os dados atualizados do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> contendo os dados do cliente atualizado,
    /// ou <c>null</c> caso o cliente não seja encontrado.
    /// </returns>
    public async Task<CustomerResponseDto?> UpdateAsync(CustomerUpdateDto customer,
        CancellationToken cancellationToken = default)
    {
        var existingCustomer = await _getCustomerByIdUseCase.ExecuteAsync(customer.Id, cancellationToken);
        if (existingCustomer is null)
            return null;

        var customerDomain = await _updateCustomerUseCase.ExecuteAsync(customer, existingCustomer, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }
}
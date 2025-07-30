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

    public static ICustomerController Create(ICustomerRepository customerRepository)
    {
        return new CustomerController(customerRepository);
    }

    /// <summary>
    /// Creates a new customer with the provided data.
    /// </summary>
    /// <param name="customer">A <see cref="CustomerRequestDto"/> object containing the data of the customer to be created.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> containing the created customer's data.
    /// </returns>
    public async Task<CustomerResponseDto> CreateAsync(CustomerRequestDto customer,
        CancellationToken cancellationToken = default)
    {
        var customerDomain = await _createCustomerUseCase.ExecuteAsync(customer, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    /// <summary>
    /// Gets a customer by the provided CPF.
    /// </summary>
    /// <param name="cpf">CPF of the customer to be retrieved.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> containing the found customer's data,
    /// or <c>null</c> if no customer exists with the provided CPF.
    /// </returns>
    public async Task<CustomerResponseDto?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _getCustomerByCpfUseCase.ExecuteAsync(cpf, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    /// <summary>
    /// Gets the data of a customer by their unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the customer.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> containing the found customer's data,
    /// or <c>null</c> if no customer exists with the provided identifier.
    /// </returns>
    public async Task<CustomerResponseDto?> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _getCustomerByIdUseCase.ExecuteAsync(id, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    /// <summary>
    /// Updates the data of an existing customer.
    /// </summary>
    /// <param name="customer">A <see cref="CustomerUpdateDto"/> object containing the updated customer data.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="CustomerResponseDto"/> containing the updated customer's data,
    /// or <c>null</c> if the customer is not found.
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
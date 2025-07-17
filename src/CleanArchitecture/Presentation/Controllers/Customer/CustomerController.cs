using Common.Dto.Customers;
using Common.Interfaces.Customer.Controller;
using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Presenter;
using TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Customer;

public class CustomerController : ICustomerController
{
    private readonly GetCustomerByCpfUseCase _getCustomerByCpfUseCase;
    private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;
    private readonly CreateCustomerUseCase _createCustomerUseCase;
    private readonly UpdateCustomerUseCase _updateCustomerUseCase;

    private readonly ICustomerPresenter _customerPresenter;

    public CustomerController(ICustomerGateway customerGateway, ICustomerPresenter customerPresenter)
    {
        _getCustomerByCpfUseCase = GetCustomerByCpfUseCase.Create(customerGateway);
        _getCustomerByIdUseCase = GetCustomerByIdUseCase.Create(customerGateway);
        _createCustomerUseCase = CreateCustomerUseCase.Create(customerGateway);
        _updateCustomerUseCase = UpdateCustomerUseCase.Create(customerGateway);

        _customerPresenter = customerPresenter;
    }

    public async Task<CustomerResponseDto> CreateAsync(CustomerRequestDto customer, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _createCustomerUseCase.ExecuteAsync(customer, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    public async Task<CustomerResponseDto?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _getCustomerByCpfUseCase.ExecuteAsync(cpf, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    public async Task<CustomerResponseDto?> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        var customerDomain = await _getCustomerByIdUseCase.ExecuteAsync(id, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }

    public async Task<CustomerResponseDto?> UpdateAsync(CustomerUpdateDto customer, CancellationToken cancellationToken = default)
    {
        var existingCustomer = await _getCustomerByIdUseCase.ExecuteAsync(customer.Id, cancellationToken);
        if (existingCustomer is null)
            return null;

        var customerDomain = await _updateCustomerUseCase.ExecuteAsync(customer, existingCustomer, cancellationToken);

        return _customerPresenter.Convert(customerDomain);
    }
}

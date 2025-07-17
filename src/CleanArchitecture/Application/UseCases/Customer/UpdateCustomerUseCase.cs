using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

public class UpdateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;
    private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;

    public UpdateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
        _getCustomerByIdUseCase = GetCustomerByIdUseCase.Create(customerGateway);
    }

    public static UpdateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new UpdateCustomerUseCase(customerGateway);
    }

    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(CustomerUpdateDto customerUpdateDto, CancellationToken cancellationToken)
    {
        var customer = await _getCustomerByIdUseCase.ExecuteAsync(customerUpdateDto.Id, cancellationToken);

        customer.UpdateCustomer(
            customerUpdateDto.Cpf,
            customerUpdateDto.Name,
            customerUpdateDto.Surname,
            customerUpdateDto.Email,
            customerUpdateDto.BirthDate,
            customerUpdateDto.IsActive);

        return await _customerGateway.UpdateAsync(customer, cancellationToken);
    }
}

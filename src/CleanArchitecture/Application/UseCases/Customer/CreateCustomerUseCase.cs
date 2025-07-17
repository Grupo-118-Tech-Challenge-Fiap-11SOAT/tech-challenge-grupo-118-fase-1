using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

public class CreateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;
    public CreateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }
    public static CreateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new CreateCustomerUseCase(customerGateway);
    }
    public async Task<Domain.Entities.Customer.Entities.Customer> ExecuteAsync(CustomerRequestDto customerRequestDto, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer.Entities.Customer(
            customerRequestDto.Cpf,
            customerRequestDto.Name,
            customerRequestDto.Surname,
            customerRequestDto.Email,
            customerRequestDto.BirthDate,
            true);

        return await _customerGateway.CreateAsync(customer, cancellationToken);
    }
}

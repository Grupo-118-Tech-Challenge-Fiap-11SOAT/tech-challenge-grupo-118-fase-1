using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

public class UpdateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    public UpdateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    public static UpdateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new UpdateCustomerUseCase(customerGateway);
    }

    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(CustomerUpdateDto customerUpdateDto, Domain.Entities.Customer.Entities.Customer customer, CancellationToken cancellationToken)
    {
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

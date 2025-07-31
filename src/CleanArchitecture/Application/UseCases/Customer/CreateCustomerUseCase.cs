using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Use case for creating a new customer.
/// </summary>
public class CreateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for customer operations.</param>
    public CreateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="CreateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for customer operations.</param>
    /// <returns>Instance of <see cref="CreateCustomerUseCase"/>.</returns>
    public static CreateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new CreateCustomerUseCase(customerGateway);
    }

    /// <summary>
    /// Executes the creation of a new customer asynchronously.
    /// </summary>
    /// <param name="customerRequestDto">DTO containing the data of the customer to be created.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Created <see cref="Domain.Entities.Customer.Entities.Customer"/> entity.</returns>
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

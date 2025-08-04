using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Use case for updating customer data.
/// </summary>
public class UpdateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for customer operations.</param>
    public UpdateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="UpdateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for customer operations.</param>
    /// <returns>Instance of <see cref="UpdateCustomerUseCase"/>.</returns>
    public static UpdateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new UpdateCustomerUseCase(customerGateway);
    }

    /// <summary>
    /// Updates the data of an existing customer.
    /// </summary>
    /// <param name="customerUpdateDto">DTO containing the updated customer data.</param>
    /// <param name="customer">Customer entity to be updated.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Updated customer or <c>null</c> if not found.</returns>
    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(
        CustomerUpdateDto customerUpdateDto,
        Domain.Entities.Customer.Entities.Customer customer,
        CancellationToken cancellationToken)
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

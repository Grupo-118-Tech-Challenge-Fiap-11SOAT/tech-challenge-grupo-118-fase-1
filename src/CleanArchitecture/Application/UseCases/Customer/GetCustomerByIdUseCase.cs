namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Use case for getting a customer by their identifier.
/// </summary>
public class GetCustomerByIdUseCase
{
    private readonly Common.Interfaces.Customer.Gateway.ICustomerGateway _customerGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="GetCustomerByIdUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for accessing customer data.</param>
    public GetCustomerByIdUseCase(Common.Interfaces.Customer.Gateway.ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="GetCustomerByIdUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for accessing customer data.</param>
    /// <returns>Instance of <see cref="GetCustomerByIdUseCase"/>.</returns>
    public static GetCustomerByIdUseCase Create(Common.Interfaces.Customer.Gateway.ICustomerGateway customerGateway)
    {
        return new GetCustomerByIdUseCase(customerGateway);
    }

    /// <summary>
    /// Gets a customer by their identifier.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Corresponding <see cref="Domain.Entities.Customer.Entities.Customer"/> entity or <c>null</c> if not found.</returns>
    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _customerGateway.GetByIdAsync(id, cancellationToken);
    }
}

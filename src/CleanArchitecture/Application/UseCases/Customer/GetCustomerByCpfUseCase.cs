using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Use case for getting a customer by CPF.
/// </summary>
public class GetCustomerByCpfUseCase
{
    private readonly ICustomerGateway _customerGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="GetCustomerByCpfUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for customer operations.</param>
    public GetCustomerByCpfUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="GetCustomerByCpfUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway for customer operations.</param>
    /// <returns>Instance of <see cref="GetCustomerByCpfUseCase"/>.</returns>
    public static GetCustomerByCpfUseCase Create(ICustomerGateway customerGateway)
    {
        return new GetCustomerByCpfUseCase(customerGateway);
    }

    /// <summary>
    /// Gets a customer by CPF asynchronously.
    /// </summary>
    /// <param name="cpf">Customer's CPF.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>
    /// An instance of <see cref="Domain.Entities.Customer.Entities.Customer"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(string cpf, CancellationToken cancellationToken)
    {
        return await _customerGateway.GetCustomerByCpf(cpf, cancellationToken);
    }
}

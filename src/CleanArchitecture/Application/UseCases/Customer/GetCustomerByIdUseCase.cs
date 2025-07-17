namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Caso de uso para obter um cliente pelo seu identificador.
/// </summary>
public class GetCustomerByIdUseCase
{
    private readonly Common.Interfaces.Customer.Gateway.ICustomerGateway _customerGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="GetCustomerByIdUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para acesso aos dados do cliente.</param>
    public GetCustomerByIdUseCase(Common.Interfaces.Customer.Gateway.ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="GetCustomerByIdUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para acesso aos dados do cliente.</param>
    /// <returns>Instância de <see cref="GetCustomerByIdUseCase"/>.</returns>
    public static GetCustomerByIdUseCase Create(Common.Interfaces.Customer.Gateway.ICustomerGateway customerGateway)
    {
        return new GetCustomerByIdUseCase(customerGateway);
    }

    /// <summary>
    /// Obtém um cliente pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Entidade <see cref="Domain.Entities.Customer.Entities.Customer"/> correspondente ou <c>null</c> se não encontrado.</returns>
    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _customerGateway.GetByIdAsync(id, cancellationToken);
    }
}

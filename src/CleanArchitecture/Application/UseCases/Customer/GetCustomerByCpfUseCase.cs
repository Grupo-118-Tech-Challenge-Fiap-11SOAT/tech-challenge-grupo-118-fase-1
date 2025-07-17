using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Caso de uso para obter um cliente pelo CPF.
/// </summary>
public class GetCustomerByCpfUseCase
{
    private readonly ICustomerGateway _customerGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="GetCustomerByCpfUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para operações de cliente.</param>
    public GetCustomerByCpfUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="GetCustomerByCpfUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para operações de cliente.</param>
    /// <returns>Instância de <see cref="GetCustomerByCpfUseCase"/>.</returns>
    public static GetCustomerByCpfUseCase Create(ICustomerGateway customerGateway)
    {
        return new GetCustomerByCpfUseCase(customerGateway);
    }

    /// <summary>
    /// Obtém um cliente pelo CPF de forma assíncrona.
    /// </summary>
    /// <param name="cpf">CPF do cliente.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>
    /// Uma instância de <see cref="Domain.Entities.Customer.Entities.Customer"/> se encontrado; caso contrário, <c>null</c>.
    /// </returns>
    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(string cpf, CancellationToken cancellationToken)
    {
        return await _customerGateway.GetCustomerByCpf(cpf, cancellationToken);
    }
}

using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Caso de uso para atualizar os dados de um cliente.
/// </summary>
public class UpdateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="UpdateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para operações de cliente.</param>
    public UpdateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="UpdateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para operações de cliente.</param>
    /// <returns>Instância de <see cref="UpdateCustomerUseCase"/>.</returns>
    public static UpdateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new UpdateCustomerUseCase(customerGateway);
    }

    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="customerUpdateDto">DTO contendo os dados atualizados do cliente.</param>
    /// <param name="customer">Entidade do cliente a ser atualizada.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Cliente atualizado ou <c>null</c> se não encontrado.</returns>
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

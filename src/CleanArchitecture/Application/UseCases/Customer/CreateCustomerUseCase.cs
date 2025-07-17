using Common.Dto.Customers;
using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

/// <summary>
/// Caso de uso para criação de um novo cliente.
/// </summary>
public class CreateCustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CreateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para operações de cliente.</param>
    public CreateCustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="CreateCustomerUseCase"/>.
    /// </summary>
    /// <param name="customerGateway">Gateway para operações de cliente.</param>
    /// <returns>Instância de <see cref="CreateCustomerUseCase"/>.</returns>
    public static CreateCustomerUseCase Create(ICustomerGateway customerGateway)
    {
        return new CreateCustomerUseCase(customerGateway);
    }

    /// <summary>
    /// Executa a criação de um novo cliente de forma assíncrona.
    /// </summary>
    /// <param name="customerRequestDto">DTO contendo os dados do cliente a ser criado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Entidade <see cref="Domain.Entities.Customer.Entities.Customer"/> criada.</returns>
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

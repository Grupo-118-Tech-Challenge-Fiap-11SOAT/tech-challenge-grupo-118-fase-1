using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Caso de uso para obter todos os funcionários.
/// </summary>
public class GetAllEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="GetAllEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    public GetAllEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="GetAllEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    /// <returns>Instância de <see cref="GetAllEmployeeUseCase"/>.</returns>
    public static GetAllEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new GetAllEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Obtém uma lista paginada de funcionários.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <param name="skip">Quantidade de registros a serem ignorados (para paginação).</param>
    /// <param name="take">Quantidade máxima de registros a serem retornados.</param>
    /// <returns>Lista de funcionários ou null se não houver registros.</returns>
    public async Task<List<Domain.Entities.Employee.Entities.Employee>?> ExecuteAsync(
        CancellationToken cancellationToken,
        int skip = 0,
        int take = 10)
    {
        return await _employeeGateway.GetAllAsync(cancellationToken, skip, take);
    }
}

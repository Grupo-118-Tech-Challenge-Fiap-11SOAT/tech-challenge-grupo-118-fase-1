namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Caso de uso para obter um funcionário pelo seu identificador.
/// </summary>
public class GetByEmployeeIdUseCase
{
    private readonly Common.Interfaces.Employee.Gateway.IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="GetByEmployeeIdUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para acesso aos dados de funcionários.</param>
    public GetByEmployeeIdUseCase(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="GetByEmployeeIdUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para acesso aos dados de funcionários.</param>
    /// <returns>Instância de <see cref="GetByEmployeeIdUseCase"/>.</returns>
    public static GetByEmployeeIdUseCase Create(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        return new GetByEmployeeIdUseCase(employeeGateway);
    }

    /// <summary>
    /// Obtém um funcionário pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// Uma instância de <see cref="Domain.Entities.Employee.Entities.Employee"/> se encontrada; caso contrário, <c>null</c>.
    /// </returns>
    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _employeeGateway.GetByIdAsync(id, cancellationToken);
    }
}

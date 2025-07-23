namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Caso de uso para obter um funcionário pelo e-mail.
/// </summary>
public class GetByEmployeeEmailUseCase
{
    private readonly Common.Interfaces.Employee.Gateway.IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="GetByEmployeeEmailUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    public GetByEmployeeEmailUseCase(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="GetByEmployeeEmailUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    /// <returns>Instância de <see cref="GetByEmployeeEmailUseCase"/>.</returns>
    public static GetByEmployeeEmailUseCase Create(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        return new GetByEmployeeEmailUseCase(employeeGateway);
    }

    /// <summary>
    /// Executa a busca de funcionário pelo e-mail informado.
    /// </summary>
    /// <param name="email">E-mail do funcionário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação.</param>
    /// <returns>Entidade <see cref="Domain.Entities.Employee.Entities.Employee"/> correspondente ou null se não encontrado.</returns>
    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(string email, CancellationToken cancellationToken)
    {
        return await _employeeGateway.GetByEmailAsync(email, cancellationToken);
    }
}

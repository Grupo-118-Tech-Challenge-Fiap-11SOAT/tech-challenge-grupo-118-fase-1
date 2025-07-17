using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Caso de uso para exclusão de um funcionário.
/// </summary>
public class DeleteEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="DeleteEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    public DeleteEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="DeleteEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    /// <returns>Instância de <see cref="DeleteEmployeeUseCase"/>.</returns>
    public static DeleteEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new DeleteEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Executa a exclusão de um funcionário pelo identificador informado.
    /// </summary>
    /// <param name="id">Identificador do funcionário a ser excluído.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// <c>true</c> se o funcionário foi excluído com sucesso; caso contrário, <c>false</c>.
    /// </returns>
    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _employeeGateway.DeleteAsync(id, cancellationToken);
    }
}

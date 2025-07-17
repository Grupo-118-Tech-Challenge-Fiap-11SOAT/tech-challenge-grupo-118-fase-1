using Common.Dto.Employee;
using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Caso de uso para atualização de dados de um funcionário.
/// </summary>
public class UpdateEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;
    private readonly GetByEmployeeIdUseCase _getByEmployeeIdUseCase;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="UpdateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    public UpdateEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
        _getByEmployeeIdUseCase = GetByEmployeeIdUseCase.Create(employeeGateway);
    }

    /// <summary>
    /// Cria uma instância de <see cref="UpdateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway para operações de funcionário.</param>
    /// <returns>Instância de <see cref="UpdateEmployeeUseCase"/>.</returns>
    public static UpdateEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new UpdateEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Atualiza os dados de um funcionário existente.
    /// </summary>
    /// <param name="updateEmployeeDto">DTO contendo os dados atualizados do funcionário.</param>
    /// <param name="employee">Entidade do funcionário a ser atualizada.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Funcionário atualizado ou <c>null</c> se não encontrado.</returns>
    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(
        UpdateEmployeeDto updateEmployeeDto,
        Domain.Entities.Employee.Entities.Employee employee,
        CancellationToken cancellationToken)
    {
        employee.UpdateEmployee(
            updateEmployeeDto.Cpf,
            updateEmployeeDto.Name,
            updateEmployeeDto.Surname,
            updateEmployeeDto.Email,
            updateEmployeeDto.BirthDate,
            updateEmployeeDto.Password,
            updateEmployeeDto.Role,
            updateEmployeeDto.IsActive);

        return await _employeeGateway.UpdateAsync(employee, cancellationToken);
    }
}

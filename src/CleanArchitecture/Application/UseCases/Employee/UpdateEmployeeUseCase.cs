using Common.Dto.Employee;
using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Use case for updating employee data.
/// </summary>
public class UpdateEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;
    private readonly GetByEmployeeIdUseCase _getByEmployeeIdUseCase;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    public UpdateEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
        _getByEmployeeIdUseCase = GetByEmployeeIdUseCase.Create(employeeGateway);
    }

    /// <summary>
    /// Creates an instance of <see cref="UpdateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    /// <returns>Instance of <see cref="UpdateEmployeeUseCase"/>.</returns>
    public static UpdateEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new UpdateEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Updates the data of an existing employee.
    /// </summary>
    /// <param name="updateEmployeeDto">DTO containing the updated employee data.</param>
    /// <param name="employee">Employee entity to be updated.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Updated employee or <c>null</c> if not found.</returns>
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

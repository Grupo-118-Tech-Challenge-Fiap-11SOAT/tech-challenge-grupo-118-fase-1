using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Use case for deleting an employee.
/// </summary>
public class DeleteEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="DeleteEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    public DeleteEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="DeleteEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    /// <returns>Instance of <see cref="DeleteEmployeeUseCase"/>.</returns>
    public static DeleteEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new DeleteEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Executes the deletion of an employee by the provided identifier.
    /// </summary>
    /// <param name="id">Identifier of the employee to be deleted.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <c>true</c> if the employee was successfully deleted; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _employeeGateway.DeleteAsync(id, cancellationToken);
    }
}

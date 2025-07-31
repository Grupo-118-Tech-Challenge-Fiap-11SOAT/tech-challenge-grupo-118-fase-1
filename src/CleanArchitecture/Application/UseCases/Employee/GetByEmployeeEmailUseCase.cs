namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Use case for getting an employee by email.
/// </summary>
public class GetByEmployeeEmailUseCase
{
    private readonly Common.Interfaces.Employee.Gateway.IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="GetByEmployeeEmailUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    public GetByEmployeeEmailUseCase(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="GetByEmployeeEmailUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    /// <returns>Instance of <see cref="GetByEmployeeEmailUseCase"/>.</returns>
    public static GetByEmployeeEmailUseCase Create(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        return new GetByEmployeeEmailUseCase(employeeGateway);
    }

    /// <summary>
    /// Executes the search for an employee by the provided email.
    /// </summary>
    /// <param name="email">Employee's email.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>Corresponding <see cref="Domain.Entities.Employee.Entities.Employee"/> entity or null if not found.</returns>
    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(string email, CancellationToken cancellationToken)
    {
        return await _employeeGateway.GetByEmailAsync(email, cancellationToken);
    }
}

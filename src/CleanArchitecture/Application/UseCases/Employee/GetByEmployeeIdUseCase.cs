namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Use case for getting an employee by their identifier.
/// </summary>
public class GetByEmployeeIdUseCase
{
    private readonly Common.Interfaces.Employee.Gateway.IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="GetByEmployeeIdUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for accessing employee data.</param>
    public GetByEmployeeIdUseCase(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="GetByEmployeeIdUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for accessing employee data.</param>
    /// <returns>Instance of <see cref="GetByEmployeeIdUseCase"/>.</returns>
    public static GetByEmployeeIdUseCase Create(Common.Interfaces.Employee.Gateway.IEmployeeGateway employeeGateway)
    {
        return new GetByEmployeeIdUseCase(employeeGateway);
    }

    /// <summary>
    /// Gets an employee by their identifier.
    /// </summary>
    /// <param name="id">Employee identifier.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// An instance of <see cref="Domain.Entities.Employee.Entities.Employee"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Domain.Entities.Employee.Entities.Employee?> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _employeeGateway.GetByIdAsync(id, cancellationToken);
    }
}

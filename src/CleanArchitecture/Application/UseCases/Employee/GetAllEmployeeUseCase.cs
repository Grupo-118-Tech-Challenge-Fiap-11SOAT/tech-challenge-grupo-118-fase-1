using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Use case for getting all employees.
/// </summary>
public class GetAllEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="GetAllEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    public GetAllEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="GetAllEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    /// <returns>Instance of <see cref="GetAllEmployeeUseCase"/>.</returns>
    public static GetAllEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new GetAllEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Gets a paginated list of employees.
    /// </summary>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <param name="skip">Number of records to skip (for pagination).</param>
    /// <param name="take">Maximum number of records to return.</param>
    /// <returns>List of employees or null if no records exist.</returns>
    public async Task<List<Domain.Entities.Employee.Entities.Employee>?> ExecuteAsync(
        CancellationToken cancellationToken,
        int skip = 0,
        int take = 10)
    {
        return await _employeeGateway.GetAllAsync(cancellationToken, skip, take);
    }
}

using Common.Dto.Employee;
using Common.Interfaces.Employee.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Employee;

/// <summary>
/// Use case for creating a new employee.
/// </summary>
public class CreateEmployeeUseCase
{
    private readonly IEmployeeGateway _employeeGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    public CreateEmployeeUseCase(IEmployeeGateway employeeGateway)
    {
        _employeeGateway = employeeGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="CreateEmployeeUseCase"/>.
    /// </summary>
    /// <param name="employeeGateway">Gateway for employee operations.</param>
    /// <returns>Instance of <see cref="CreateEmployeeUseCase"/>.</returns>
    public static CreateEmployeeUseCase Create(IEmployeeGateway employeeGateway)
    {
        return new CreateEmployeeUseCase(employeeGateway);
    }

    /// <summary>
    /// Executes the use case to create a new employee.
    /// </summary>
    /// <param name="employeeRequestDto">DTO containing the data of the employee to be created.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Created <see cref="Domain.Entities.Employee.Entities.Employee"/> entity.</returns>
    public async Task<Domain.Entities.Employee.Entities.Employee> ExecuteAsync(EmployeeRequestDto employeeRequestDto, CancellationToken cancellationToken)
    {
        var employee = new Domain.Entities.Employee.Entities.Employee(
            employeeRequestDto.Cpf,
            employeeRequestDto.Name,
            employeeRequestDto.Surname,
            employeeRequestDto.Email,
            employeeRequestDto.BirthDay,
            employeeRequestDto.Password,
            employeeRequestDto.Role,
            true);

        return await _employeeGateway.CreateAsync(employee, cancellationToken);
    }
}

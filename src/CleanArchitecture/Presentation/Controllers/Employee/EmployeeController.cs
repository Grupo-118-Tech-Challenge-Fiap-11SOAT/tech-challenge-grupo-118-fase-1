using Common.Dto.Employee;
using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Controller;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Presenter;
using Common.Interfaces.Employee.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Employee;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Employee;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Employee;

public class EmployeeController : IEmployeeController
{
    private readonly CreateEmployeeUseCase _createEmployeeUseCase;
    private readonly GetAllEmployeeUseCase _getAllEmployeeUseCase;
    private readonly GetByEmployeeIdUseCase _getEmployeeByIdUseCase;
    private readonly UpdateEmployeeUseCase _updateEmployeeUseCase;
    private readonly DeleteEmployeeUseCase _deleteEmployeeUseCase;

    private readonly IEmployeePresenter _employeePresenter;

    public EmployeeController(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        IEmployeeGateway employeeGateway = EmployeeGateway.Create(employeeRepository, passwordManager);

        _createEmployeeUseCase = CreateEmployeeUseCase.Create(employeeGateway);
        _getAllEmployeeUseCase = GetAllEmployeeUseCase.Create(employeeGateway);
        _getEmployeeByIdUseCase = GetByEmployeeIdUseCase.Create(employeeGateway);
        _updateEmployeeUseCase = UpdateEmployeeUseCase.Create(employeeGateway);
        _deleteEmployeeUseCase = DeleteEmployeeUseCase.Create(employeeGateway);

        _employeePresenter = EmployeePresenter.Create();
    }

    public static IEmployeeController Create(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        return new EmployeeController(employeeRepository, passwordManager);
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employee">DTO containing the data of the employee to be created.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="EmployeeResponseDto"/> representing the created employee.
    /// </returns>
    public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto employee,
        CancellationToken cancellationToken = default)
    {
        var createdEmployee = await _createEmployeeUseCase.ExecuteAsync(employee, cancellationToken);

        return _employeePresenter.Convert(createdEmployee);
    }

    /// <summary>
    /// Deletes an employee by the provided identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the employee to be deleted.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <c>true</c> if the employee was successfully deleted; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deleteEmployeeUseCase = await _deleteEmployeeUseCase.ExecuteAsync(id, cancellationToken);

        return deleteEmployeeUseCase;
    }

    /// <summary>
    /// Retrieves a paginated list of employees.
    /// </summary>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <param name="skip">Number of records to skip for pagination.</param>
    /// <param name="take">Maximum number of records to return.</param>
    /// <returns>
    /// A list of <see cref="EmployeeResponseDto"/> representing the found employees, or <c>null</c> if no employees are found.
    /// </returns>
    public async Task<List<EmployeeResponseDto>?> GetAllAsync(CancellationToken cancellationToken = default,
        int skip = 0, int take = 10)
    {
        var employees = await _getAllEmployeeUseCase.ExecuteAsync(cancellationToken, skip, take);

        return _employeePresenter.Convert(employees);
    }

    /// <summary>
    /// Retrieves an employee by the provided identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the employee.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="EmployeeResponseDto"/> representing the found employee, or <c>null</c> if not found.
    /// </returns>
    public async Task<EmployeeResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _getEmployeeByIdUseCase.ExecuteAsync(id, cancellationToken);

        return _employeePresenter.Convert(employee);
    }

    /// <summary>
    /// Updates the data of an existing employee.
    /// </summary>
    /// <param name="employee">DTO containing the updated data of the employee.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// <see cref="EmployeeResponseDto"/> representing the updated employee, or <c>null</c> if the employee does not exist.
    /// </returns>
    public async Task<EmployeeResponseDto?> UpdateAsync(UpdateEmployeeDto employee,
        CancellationToken cancellationToken = default)
    {
        var existingEmployee = await _getEmployeeByIdUseCase.ExecuteAsync(employee.Id, cancellationToken);
        if (existingEmployee is null)
        {
            return null;
        }

        var updateEmployeeUseCase =
            await _updateEmployeeUseCase.ExecuteAsync(employee, existingEmployee, cancellationToken);

        return _employeePresenter.Convert(updateEmployeeUseCase);
    }
}
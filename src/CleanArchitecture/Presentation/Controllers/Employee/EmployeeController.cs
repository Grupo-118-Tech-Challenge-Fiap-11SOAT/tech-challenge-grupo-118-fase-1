using Common.Dto.Employee;
using Common.Interfaces.Employee.Controller;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Presenter;
using TechChallengeFastFood.CleanArch.Application.UseCases.Employee;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Employee;

public class EmployeeController : IEmployeeController
{
    private readonly CreateEmployeeUseCase _createEmployeeUseCase;
    private readonly GetAllEmployeeUseCase _getAllEmployeeUseCase;
    private readonly GetByEmployeeIdUseCase _getEmployeeByIdUseCase;
    private readonly UpdateEmployeeUseCase _updateEmployeeUseCase;
    private readonly DeleteEmployeeUseCase _deleteEmployeeUseCase;

    private readonly IEmployeePresenter _employeePresenter;

    public EmployeeController(IEmployeeGateway employeeGateway, IEmployeePresenter employeePresenter)
    {
        _createEmployeeUseCase = CreateEmployeeUseCase.Create(employeeGateway);
        _getAllEmployeeUseCase = GetAllEmployeeUseCase.Create(employeeGateway);
        _getEmployeeByIdUseCase = GetByEmployeeIdUseCase.Create(employeeGateway);
        _updateEmployeeUseCase = UpdateEmployeeUseCase.Create(employeeGateway);
        _deleteEmployeeUseCase = DeleteEmployeeUseCase.Create(employeeGateway);

        _employeePresenter = employeePresenter;
    }

    public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto employee, CancellationToken cancellationToken = default)
    {
        var createdEmployee = await _createEmployeeUseCase.ExecuteAsync(employee, cancellationToken);

        return _employeePresenter.Convert(createdEmployee);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deleteEmployeeUseCase = await _deleteEmployeeUseCase.ExecuteAsync(id, cancellationToken);

        return deleteEmployeeUseCase;
    }

    public async Task<List<EmployeeResponseDto>?> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var employees = await _getAllEmployeeUseCase.ExecuteAsync(cancellationToken, skip, take);

        return _employeePresenter.Convert(employees);
    }

    public async Task<EmployeeResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _getEmployeeByIdUseCase.ExecuteAsync(id, cancellationToken);

        return _employeePresenter.Convert(employee);
    }

    public async Task<EmployeeResponseDto?> UpdateAsync(UpdateEmployeeDto employee, CancellationToken cancellationToken = default)
    {
        var existingEmployee = await _getEmployeeByIdUseCase.ExecuteAsync(employee.Id, cancellationToken);
        if (existingEmployee is null)
        {
            return null;
        }

        var updateEmployeeUseCase = await _updateEmployeeUseCase.ExecuteAsync(employee, existingEmployee, cancellationToken);

        return _employeePresenter.Convert(updateEmployeeUseCase);
    }
}

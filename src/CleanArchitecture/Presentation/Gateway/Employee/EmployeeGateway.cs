using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Repositories;
using EmployeeDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Entities.Employee;
using EmployeeEntity = Common.Dto.Employee.Database.Employee;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;

public class EmployeeGateway : IEmployeeGateway
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeGateway(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDomain> CreateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default)
    {
        var employeeEntity = new EmployeeEntity
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            employee.Password,
            employee.Role,
            employee.IsActive
        );

        var createdEmployee = await _employeeRepository.CreateAsync(employeeEntity, cancellationToken);

        return new EmployeeDomain
        (
            createdEmployee.Cpf,
            createdEmployee.Name,
            createdEmployee.Surname,
            createdEmployee.Email,
            createdEmployee.BirthDay,
            createdEmployee.Password,
            createdEmployee.Role,
            createdEmployee.IsActive,
            createdEmployee.Id
        );
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _employeeRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<List<EmployeeDomain>?> GetAllAsync(CancellationToken cancellationToken = default, int skip = 0, int take = 10)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken, skip, take);

        if (employees is null)
            return null;

        var employeeDtos = new List<EmployeeDomain>();
        employees.ForEach(employeeEntity =>
        {
            employeeDtos.Add(new EmployeeDomain
            (
                employeeEntity.Cpf,
                employeeEntity.Name,
                employeeEntity.Surname,
                employeeEntity.Email,
                employeeEntity.BirthDay,
                employeeEntity.Password,
                employeeEntity.Role,
                employeeEntity.IsActive,
                employeeEntity.Id
            ));
        });

        return employeeDtos;
    }

    public async Task<EmployeeDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        return new EmployeeDomain
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            employee.Password,
            employee.Role,
            employee.IsActive,
            employee.Id
        );
    }

    public async Task<EmployeeDomain?> UpdateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default)
    {
        var employeeEntity = new EmployeeEntity
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            employee.Password,
            employee.Role,
            employee.IsActive,
            employee.Id
        );

        await _employeeRepository.UpdateAsync(employeeEntity, cancellationToken);

        return employee;
    }
}

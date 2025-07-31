using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Repositories;
using System.Globalization;
using EmployeeDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Entities.Employee;
using EmployeeEntity = Common.Dto.Employee.Database.Employee;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;

public class EmployeeGateway : IEmployeeGateway
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordManager _passwordManager;

    public EmployeeGateway(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        _employeeRepository = employeeRepository;
        _passwordManager = passwordManager;
    }

    public static IEmployeeGateway Create(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        return new EmployeeGateway(employeeRepository, passwordManager);
    }

    /// <summary>
    /// Creates a new employee in the repository.
    /// </summary>
    /// <param name="employee">Domain object of the employee to be created.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Returns the created employee as a domain object.</returns>
    public async Task<EmployeeDomain> CreateAsync(EmployeeDomain employee, CancellationToken cancellationToken = default)
    {
        _passwordManager.CreatePasswordHash(employee.Password, out var hashedPassword);
        var employeeEntity = new EmployeeEntity
        (
            employee.Cpf,
            employee.Name,
            employee.Surname,
            employee.Email,
            employee.BirthDay,
            hashedPassword,
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

    /// <summary>
    /// Deletes an employee from the repository by identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the employee to be deleted.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>Returns <c>true</c> if the deletion was successful.</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _employeeRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a paginated list of employees from the repository.
    /// </summary>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <param name="skip">Number of records to skip for pagination.</param>
    /// <param name="take">Maximum number of records to return.</param>
    /// <returns>
    /// Returns a list of <see cref="EmployeeDomain"/> domain objects representing the employees,
    /// or <c>null</c> if no records exist.
    /// </returns>
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

    public async Task<EmployeeDomain?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByEmailAsync(email, cancellationToken);

        if (employee is null)
            return null;

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

    /// <summary>
    /// Retrieves an employee from the repository by unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the employee.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// Returns the <see cref="EmployeeDomain"/> domain object representing the found employee,
    /// or <c>null</c> if no employee exists with the provided identifier.
    /// </returns>
    public async Task<EmployeeDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee is null)
            return null;

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

    /// <summary>
    /// Updates the data of an existing employee in the repository.
    /// </summary>
    /// <param name="employee">Domain object of the employee with updated data.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// Returns the <see cref="EmployeeDomain"/> domain object representing the updated employee,
    /// or <c>null</c> if the update is not successful.
    /// </returns>
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

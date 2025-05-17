using Domain.Employee.Dtos;
using Domain.Employee.Exceptions;
using Domain.Employee.Ports;
using Domain.Employee.Ports.Out;

namespace Application.Employee;

public class EmployeeManager : IEmployeeManager
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeManager(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Creates a new employee based on the provided <see cref="EmployeeDto"/>.
    /// </summary>
    /// <param name="employeeDto">The data transfer object containing employee details.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created <see cref="EmployeeDto"/>.
    /// If an exception occurs, the returned <see cref="EmployeeDto"/> will contain error details.
    /// </returns>
    /// <exception cref="CpfNullOrEmptyException">Thrown when the CPF is null or empty.</exception>
    /// <exception cref="NameNullOrEmptyException">Thrown when the name is null or empty.</exception>
    /// <exception cref="SurnameNullOrEmptyException">Thrown when the surname is null or empty.</exception>
    /// <exception cref="EmailNullOrEmptyException">Thrown when the email is null or empty.</exception>
    /// <exception cref="BirthDayMinValueException">Thrown when the birth date is invalid.</exception>
    /// <exception cref="PasswordNullOrEmptyException">Thrown when the password is null or empty.</exception>
    public async Task<EmployeeDto> Create(EmployeeDto employeeDto)
    {
        try
        {
            var employee = EmployeeDto.ToEntity(employeeDto);

            await employee.Save(_employeeRepository);
            employeeDto.Id = employee.Id;

            return employeeDto;
        }
        catch (Exception ex) when (ex is CpfNullOrEmptyException ||
                                   ex is NameNullOrEmptyException ||
                                   ex is SurnameNullOrEmptyException ||
                                   ex is EmailNullOrEmptyException ||
                                   ex is BirthDayMinValueException ||
                                   ex is PasswordNullOrEmptyException)
        {
            return new EmployeeDto
            {
                ErrorMessage = ex.Message,
                Error = true
            };
        }
        catch (Exception ex)
        {
            return new EmployeeDto
            {
                ErrorMessage = ex.Message,
                Error = true
            };
        }
    }

    /// <summary>
    /// Deletes an employee by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains:
    /// <list type="bullet">
    /// <item><description>0 if the employee does not exist.</description></item>
    /// <item><description>The number of records affected if the employee is successfully deleted.</description></item>
    /// </list>
    /// </returns>
    public async Task<int> Delete(int id)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
        {
            return 0;
        }

        return await _employeeRepository.Delete(employee);
    }

    /// <summary>  
    /// Retrieves all employees from the repository.  
    /// </summary>  
    /// <returns>  
    /// A task that represents the asynchronous operation. The task result contains a list of <see cref="EmployeeDto"/> objects.  
    /// </returns>  
    public async Task<List<EmployeeDto>> GetAll()
    {
        var employeeList = await _employeeRepository.GetAll();
        var result = new List<EmployeeDto>(employeeList.Count);
        foreach (var employee in employeeList)
        {
            result.Add(new EmployeeDto(employee));
        }
        return result;
    }

    /// <summary>  
    /// Retrieves an employee by their unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the employee to retrieve.</param>  
    /// <returns>  
    /// A task that represents the asynchronous operation. The task result contains the <see cref="EmployeeDto"/> object  
    /// representing the employee if found, or an empty <see cref="EmployeeDto"/> if the employee does not exist.  
    /// </returns>  
    public async Task<EmployeeDto> GetById(int id)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
        {
            return new EmployeeDto
            {
                ErrorMessage = "Employee not found.",
                Error = true
            };
        }

        return EmployeeDto.ToDto(employee);
    }

    /// <summary>  
    /// Updates an existing employee based on the provided <see cref="EmployeeDto"/>.  
    /// </summary>  
    /// <param name="employeeDto">The data transfer object containing updated employee details.</param>  
    /// <returns>  
    /// A task that represents the asynchronous operation. The task result contains the updated <see cref="EmployeeDto"/>.  
    /// If an exception occurs, the returned <see cref="EmployeeDto"/> will contain error details.  
    /// </returns>  
    /// <exception cref="CpfNullOrEmptyException">Thrown when the CPF is null or empty.</exception>  
    /// <exception cref="NameNullOrEmptyException">Thrown when the name is null or empty.</exception>  
    /// <exception cref="SurnameNullOrEmptyException">Thrown when the surname is null or empty.</exception>  
    /// <exception cref="EmailNullOrEmptyException">Thrown when the email is null or empty.</exception>  
    /// <exception cref="BirthDayMinValueException">Thrown when the birth date is invalid.</exception>  
    /// <exception cref="PasswordNullOrEmptyException">Thrown when the password is null or empty.</exception>  
    public async Task<EmployeeDto> Update(EmployeeDto employeeDto)
    {
        try
        {
            var employee = await _employeeRepository.GetById(employeeDto.Id);

            if (employee == null)
            {
                return new EmployeeDto
                {
                    ErrorMessage = "Employee not found.",
                    Error = true
                };
            }

            employee.Cpf = employeeDto.Cpf;
            employee.Name = employeeDto.Name;
            employee.Surname = employeeDto.Surname;
            employee.Email = employeeDto.Email;
            employee.BirthDay = employeeDto.BirthDay;
            employee.Password = employeeDto.Password;
            employee.Role = employeeDto.Role;
            employee.IsActive = employeeDto.IsActive;
            employee.UpdatedAt = DateTime.UtcNow;

            var updatedEmployee = await _employeeRepository.Update(employee);

            return EmployeeDto.ToDto(updatedEmployee);
        }
        catch (Exception ex) when (ex is CpfNullOrEmptyException ||
                                   ex is NameNullOrEmptyException ||
                                   ex is SurnameNullOrEmptyException ||
                                   ex is EmailNullOrEmptyException ||
                                   ex is BirthDayMinValueException ||
                                   ex is PasswordNullOrEmptyException)
        {
            return new EmployeeDto
            {
                ErrorMessage = ex.Message,
                Error = true
            };
        }
        catch (Exception ex)
        {
            return new EmployeeDto
            {
                ErrorMessage = ex.Message,
                Error = true
            };
        }
    }
}

using Domain.Employee.Dtos;
using Domain.Employee.Exceptions;
using Domain.Employee.Ports.In;
using Domain.Employee.Ports.Out;

namespace Application.Employee;

public class EmployeeManager : IEmployeeManager
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordManager _passwordManager;

    public EmployeeManager(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        _employeeRepository = employeeRepository;
        _passwordManager = passwordManager;
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
    public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        try
        {
            var employee = EmployeeDto.ToEntity(employeeDto);

            employee.ValidateEmployee();
            _passwordManager.CreatePasswordHash(employeeDto.Password, out var passwordHash);
            employee.Password = passwordHash.ToString();
            await _employeeRepository.CreateAsync(employee, cancellationToken);

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
                ErrorMessage = $"Message: {ex.Message}",
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
    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        if (employee == null)
        {
            return 0;
        }

        return await _employeeRepository.DeleteAsync(employee, cancellationToken);
    }

    /// <summary>  
    /// Retrieves all employees from the repository.  
    /// </summary>  
    /// <returns>  
    /// A task that represents the asynchronous operation. The task result contains a list of <see cref="EmployeeDto"/> objects.  
    /// </returns>  
    public async Task<List<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken, int skip = 0, int take = 10)
    {
        var employeeList = await _employeeRepository.GetAllAsync(cancellationToken, skip, take);

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
    public async Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
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
    public async Task<EmployeeDto> UpdateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeDto.Id, cancellationToken);

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

            employee.ValidateEmployee();

            var updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);

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
                ErrorMessage = $"Message: {ex.Message}",
                Error = true
            };
        }
    }

    /// <summary>  
    /// Authenticates an employee using their email and password.  
    /// </summary>  
    /// <param name="email">The email address of the employee.</param>  
    /// <param name="password">The password of the employee.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>  
    /// A task that represents the asynchronous operation. The task result contains a JWT token if authentication is successful.  
    /// </returns>  
    /// <exception cref="Exception">Thrown when the employee is not found or the password is invalid.</exception>  
    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByEmailAsync(email, cancellationToken);
        if (employee == null)
        {
            throw new ArgumentNullException("Employee not found.");
        }

        if (!_passwordManager.VerifyPassword(password, employee.Password))
        {
            throw new Exception("Invalid password.");
        }

        return _passwordManager.CreateToken(EmployeeDto.ToDto(employee));
    }
}

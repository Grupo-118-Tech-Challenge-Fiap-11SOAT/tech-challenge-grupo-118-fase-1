using Common.Dto.Employee;
using Common.Interfaces.Employee.Controller;
using Infra.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFastFood.CleanArch.Infrastructure.Database;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Repositories;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Controller responsible for managing employee operations.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeController _employeeController;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeController"/> class.
    /// </summary>
    /// <param name="cleanArchDbContext"></param>
    /// <param name="configuration"></param>

    public EmployeeController(CleanArchDbContext cleanArchDbContext, IConfiguration configuration)
    {
        var passwordManager = PasswordManager.Create(configuration);
        
        var employeeRepository = EmployeeRepository.Create(cleanArchDbContext);
        _employeeController = Presentation.Controllers.Employee.EmployeeController.Create(employeeRepository, passwordManager);
    }

    private readonly ProblemDetails EMPLOYEE_NOT_FOUND = new ProblemDetails
    {
        Title = "Employee not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The requested employee could not be found."
    };

    /// <summary>
    /// Get a specific employee by their ID.
    /// </summary>
    /// <param name="id">Employee ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Valid employee</returns>
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeController.GetByIdAsync(id, cancellationToken);
        if (employee is null)
            return NotFound(EMPLOYEE_NOT_FOUND);

        return Ok(employee);
    }

    /// <summary>
    /// Get all employees with pagination support.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="skip">Registers count to skip</param>
    /// <param name="take">Registers count to take</param>
    /// <returns>Employee list</returns>
    [ProducesResponseType(typeof(List<EmployeeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int skip = 0, int take = 10)
    {
        var employees = await _employeeController.GetAllAsync(cancellationToken, skip, take);
        if (employees is null || employees.Count == 0)
            return NotFound(EMPLOYEE_NOT_FOUND);

        return Ok(employees);
    }

    /// <summary>
    /// Creates a new employee with the provided data.
    /// </summary>
    /// <param name="employeeDto">Employee data.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created employee</returns>
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] EmployeeRequestDto employeeDto,
        CancellationToken cancellationToken)
    {
        var result = await _employeeController.CreateAsync(employeeDto, cancellationToken);
        return CreatedAtAction("GetEmployeeById", new { result.Id }, result);
    }

    /// <summary>
    /// Updates an existing employee with the provided data.
    /// </summary>
    /// <param name="id">Employee ID</param>
    /// <param name="employeeDto">Employee updated data</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated employee</returns>
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EmployeeResponseDto), StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateEmployeeDto employeeDto,
        CancellationToken cancellationToken)
    {
        var result = await _employeeController.UpdateAsync(employeeDto, cancellationToken);
        if (result is null)
            return NotFound(EMPLOYEE_NOT_FOUND);
        return Ok(result);
    }

    /// <summary>
    /// Remove an employee by their ID.
    /// </summary>
    /// <param name="id">Employee ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _employeeController.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
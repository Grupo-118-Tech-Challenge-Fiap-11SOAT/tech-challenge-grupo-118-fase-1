using Domain.Employee.Dtos;
using Domain.Employee.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeManager _employeeManager;

    /// <summary>  
    /// Initializes a new instance of the <see cref="EmployeeController"/> class.  
    /// </summary>  
    /// <param name="employeeManager">The employee manager service used to handle employee-related operations.</param>  
    public EmployeeController(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    /// <summary>  
    /// Creates a new employee in the system.  
    /// </summary>  
    /// <param name="employeeDto">The data transfer object containing the details of the employee to be created.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>An <see cref="EmployeeDto"/> object representing the created employee, or a 400 status if the creation fails.</returns>  
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var result = await _employeeManager.CreateAsync(employeeDto, cancellationToken);

        if (result.Error)
        {
            return BadRequest(result);
        }

        return Created("", result);
    }

    /// <summary>  
    /// Updates an existing employee.  
    /// </summary>  
    /// <param name="id">The unique identifier of the employee to update.</param>  
    /// <param name="employeeDto">The updated employee data.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>An <see cref="EmployeeDto"/> object representing the updated employee, or a 400 status if the update fails.</returns>  
    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> Update(int id, [FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        employeeDto.Id = id;
        var result = await _employeeManager.UpdateAsync(employeeDto, cancellationToken);

        if (result.Error)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>  
    /// Deletes an employee by their unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the employee to delete.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>The ID of the deleted employee if successful.</returns>  
    [HttpDelete]
    public async Task<ActionResult<int>> Delete([FromQuery] int id, CancellationToken cancellationToken)
    {
        var result = await _employeeManager.DeleteAsync(id, cancellationToken);

        return Ok(result);
    }

    /// <summary>  
    /// Retrieves a paginated list of all employees.  
    /// </summary>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <param name="skip">The number of records to skip for pagination. Default is 0.</param>  
    /// <param name="take">The number of records to take for pagination. Default is 10.</param>  
    /// <returns>A list of <see cref="EmployeeDto"/> objects representing the employees.</returns>  
    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll(CancellationToken cancellationToken, int skip = 0, int take = 10)
    {
        return Ok(await _employeeManager.GetAllAsync(cancellationToken, skip, take));
    }

    /// <summary>  
    /// Retrieves an employee by their unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the employee to retrieve.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>An <see cref="EmployeeDto"/> object representing the employee, or a 404 status if not found.</returns>  
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok(await _employeeManager.GetByIdAsync(id, cancellationToken));
    }
}

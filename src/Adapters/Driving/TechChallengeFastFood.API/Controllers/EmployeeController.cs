using Domain.Employee.Dtos;
using Domain.Employee.Ports;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeManager _employeeManager;
    public EmployeeController(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employeeDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto employeeDto)
    {
        var result = await _employeeManager.Create(employeeDto);

        if (result.Error)
        {
            return BadRequest(result);
        }

        return Created("", result);
    }

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="employeeDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("id")]
    public async Task<ActionResult<EmployeeDto>> Update(int id, [FromBody] EmployeeDto employeeDto)
    {
        employeeDto.Id = id;
        var result = await _employeeManager.Update(employeeDto);

        if (result.Error)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an employee by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult<EmployeeDto>> Delete([FromQuery] int id)
    {
        var result = await _employeeManager.Delete(id);

        return Ok(result);
    }

    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll()
    {
        return Ok(await _employeeManager.GetAll());
    }

    /// <summary>
    /// Gets an employee by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        return Ok(await _employeeManager.GetById(id));
    }
}

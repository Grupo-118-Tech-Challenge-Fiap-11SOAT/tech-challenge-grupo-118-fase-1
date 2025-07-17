using Common.Dto.Employee;
using Common.Interfaces.Employee.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeController _employeeController;

    public EmployeeController(IEmployeeController employeeController)
    {
        _employeeController = employeeController;
    }

    private readonly ProblemDetails EMPLOYEE_NOT_FOUND = new ProblemDetails
    {
        Title = "Employee not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The requested employee could not be found."
    };

    public async Task<IActionResult> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeController.GetByIdAsync(id, cancellationToken);
        if (employee is null)
            return NotFound(EMPLOYEE_NOT_FOUND);

        return Ok(employee);
    }

    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int skip = 0, int take = 10)
    {
        var employees = await _employeeController.GetAllAsync(cancellationToken, skip, take);
        if (employees is null || employees.Count == 0)
            return NotFound(EMPLOYEE_NOT_FOUND);

        return Ok(employees);
    }

    public async Task<IActionResult> PostAsync([FromBody] EmployeeRequestDto employeeDto, CancellationToken cancellationToken)
    {
        var result = await _employeeController.CreateAsync(employeeDto, cancellationToken);
        return CreatedAtAction("GetEmployeeById", new { result.Id }, result);
    }

    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateEmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var result = await _employeeController.UpdateAsync(employeeDto, cancellationToken);
        if (result is null)
            return NotFound(EMPLOYEE_NOT_FOUND);
        return Ok(result);
    }

    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _employeeController.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
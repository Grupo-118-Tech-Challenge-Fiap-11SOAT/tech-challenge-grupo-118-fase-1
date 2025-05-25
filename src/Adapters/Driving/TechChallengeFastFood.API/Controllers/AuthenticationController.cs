using Domain.Employee.Dtos;
using Domain.Employee.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : Controller
{
    private readonly IEmployeeManager _employeeManager;
    public AuthenticationController(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var employee = await _employeeManager.CreateAsync(employeeDto, cancellationToken);
        return CreatedAtAction(nameof(Register), new { id = employee.Id }, employee);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password, CancellationToken cancellationToken)
    {
        var token = await _employeeManager.Login(email, password, cancellationToken);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        return Ok(new { Token = token });
    }
}

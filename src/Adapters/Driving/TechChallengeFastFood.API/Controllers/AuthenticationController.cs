using Domain.Base.Dtos;
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

    /// <summary>
    /// Registers a new employee in the system.
    /// </summary>
    /// <param name="employeeDto">The data transfer object containing employee details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IActionResult"/> containing the registered employee's details.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var employee = await _employeeManager.CreateAsync(employeeDto, cancellationToken);
        return Ok(new LoginResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email
        });
    }

    /// <summary>
    /// Authenticates an employee and generates a token if the credentials are valid.
    /// </summary>
    /// <param name="loginDto">The data transfer object containing the login credentials (email and password).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a token if authentication is successful, 
    /// or an <see cref="UnauthorizedResult"/> if the credentials are invalid.
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var token = await _employeeManager.Login(loginDto.Email, loginDto.Password, cancellationToken);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        return Ok(new { Token = token });
    }
}

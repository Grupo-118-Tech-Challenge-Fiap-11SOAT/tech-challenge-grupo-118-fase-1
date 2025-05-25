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

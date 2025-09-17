using Common.Dto.Employee;
using Common.Dto.Login;
using Common.Interfaces.Employee.Controller;
using Infra.Password;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFastFood.CleanArch.Infrastructure.Database;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Repositories;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Controller for handling employee authentication operations such as registration and login.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationController _authenticationController;

    public AuthenticationController(CleanArchDbContext dbContext, IConfiguration configuration)
    {
        var employeeRepository = EmployeeRepository.Create(dbContext);
        var passwordManager = PasswordManager.Create(configuration);

        _authenticationController =
            Presentation.Controllers.Employee.AuthenticationController.Create(employeeRepository, passwordManager);
    }

    /// <summary>
    /// Registers a new employee with the provided details.
    /// </summary>
    /// <param name="employeeRequestDto">DTO with data</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>Return the created employee.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] EmployeeRequestDto employeeRequestDto,
        CancellationToken cancellationToken)
    {
        var createdEmployee = await _authenticationController.RegisterAsync(employeeRequestDto, cancellationToken);
        return Ok(createdEmployee);
    }


    /// <summary>
    /// Execute the login operation for an employee with the provided credentials.
    /// </summary>
    /// <param name="loginRequestDto">DTO containing the employee's email and password.</param>
    /// <param name="cancellationToken">Token for cancelling the asynchronous operation.</param>
    /// <returns>
    /// Returns an authentication token if the login is successful.
    /// If the employee is not found, returns <see cref="NotFound"/>.
    /// If the password is invalid or the token is not generated, returns <see cref="Unauthorized"/>.
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequestDto,
        CancellationToken cancellationToken)
    {
        var token = await _authenticationController.LoginAsync(loginRequestDto, cancellationToken);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid credentials.");
        }

        return Ok(token);
    }
}
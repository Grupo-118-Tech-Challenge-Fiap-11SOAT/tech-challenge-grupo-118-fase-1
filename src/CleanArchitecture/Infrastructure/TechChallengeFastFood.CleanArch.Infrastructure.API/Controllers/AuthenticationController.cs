using Common.Dto.Employee;
using Common.Dto.Login;
using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Presenter;
using Common.Interfaces.Employee.Repositories;
using Common.Interfaces.Login.Gateway;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFastFood.CleanArch.Application.UseCases.Employee;
using TechChallengeFastFood.CleanArch.Application.UseCases.Login;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Login;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : Controller
{
    private readonly CreateEmployeeUseCase _createEmployeeUseCase;
    private readonly GetByEmployeeEmailUseCase _getByEmployeeEmailUseCase;
    private readonly LoginUseCase _loginUseCase;
    private readonly VerifyPasswordUseCase _verifyPasswordUseCase;

    private readonly IEmployeePresenter _employeePresenter;

    public AuthenticationController(IEmployeeRepository employeeRepository, IPasswordManager passwordManager)
    {
        IEmployeeGateway employeeGateway = EmployeeGateway.Create(employeeRepository, passwordManager);
        ILoginGateway loginGateway = LoginGateway.Create(passwordManager);

        _createEmployeeUseCase = CreateEmployeeUseCase.Create(employeeGateway);
        _getByEmployeeEmailUseCase = GetByEmployeeEmailUseCase.Create(employeeGateway);
        _loginUseCase = LoginUseCase.Create(loginGateway);
        _verifyPasswordUseCase = VerifyPasswordUseCase.Create(loginGateway);
    }

    /// <summary>
    /// Realiza o registro de um novo funcionário.
    /// </summary>
    /// <param name="employeeRequestDto">DTO contendo os dados do funcionário a ser registrado.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna os dados do funcionário criado.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] EmployeeRequestDto employeeRequestDto, CancellationToken cancellationToken)
    {
        var createdEmployee = await _createEmployeeUseCase.ExecuteAsync(employeeRequestDto, cancellationToken);
        return Ok(_employeePresenter.Convert(createdEmployee));
    }


    /// <summary>
    /// Realiza o login de um funcionário utilizando as credenciais fornecidas.
    /// </summary>
    /// <param name="loginRequestDto">DTO contendo o e-mail e a senha do funcionário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>
    /// Retorna um token de autenticação caso o login seja bem-sucedido.
    /// Caso o funcionário não seja encontrado, retorna <see cref="NotFound"/>.
    /// Caso a senha seja inválida ou o token não seja gerado, retorna <see cref="Unauthorized"/>.
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        var employee = await _getByEmployeeEmailUseCase.ExecuteAsync(loginRequestDto.Email, cancellationToken);

        if (employee == null || !_verifyPasswordUseCase.Execute(loginRequestDto.Password, employee.Password))
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = _loginUseCase.Execute(employee.Id, employee.Name, employee.Role);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}
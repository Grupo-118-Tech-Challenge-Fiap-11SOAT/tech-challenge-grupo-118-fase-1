using Common.Dto.Employee;
using Common.Dto.Login;
using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Controller;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Presenter;
using Common.Interfaces.Employee.Repositories;
using Common.Interfaces.Login.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Employee;
using TechChallengeFastFood.CleanArch.Application.UseCases.Login;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Login;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Employee;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Employee;

public class AuthenticationController : IAuthenticationController
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

        _employeePresenter = EmployeePresenter.Create();
    }

    public static IAuthenticationController Create(IEmployeeRepository employeeRepository,
        IPasswordManager passwordManager)
    {
        return new AuthenticationController(employeeRepository, passwordManager);
    }

    public async Task<EmployeeResponseDto?> RegisterAsync(EmployeeRequestDto employeeRequestDto,
        CancellationToken cancellationToken = default)
    {
        var createdEmployee = await _createEmployeeUseCase.ExecuteAsync(employeeRequestDto, cancellationToken);

        return createdEmployee is null ? null : _employeePresenter.Convert(createdEmployee);
    }

    public async Task<string> LoginAsync(LoginRequestDto loginRequestDto,
        CancellationToken cancellationToken = default)
    {
        var employee = await _getByEmployeeEmailUseCase.ExecuteAsync(loginRequestDto.Email, cancellationToken);

        if (employee == null || !_verifyPasswordUseCase.Execute(loginRequestDto.Password, employee.Password))
        {
            return null;
        }

        var token = _loginUseCase.Execute(employee.Id, employee.Name, employee.Role);
        return token;
    }
}
using Common.Enums;
using Common.Interfaces.Login.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Login;

/// <summary>
/// Use case responsible for performing employee login.
/// </summary>
public class LoginUseCase
{
    private readonly ILoginGateway _loginGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="LoginUseCase"/>.
    /// </summary>
    /// <param name="loginGateway">Gateway responsible for login authentication.</param>
    public LoginUseCase(ILoginGateway loginGateway)
    {
        _loginGateway = loginGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="LoginUseCase"/> using the provided gateway.
    /// </summary>
    /// <param name="loginGateway">Gateway responsible for login authentication.</param>
    /// <returns>Instance of <see cref="LoginUseCase"/>.</returns>
    public static LoginUseCase Create(ILoginGateway loginGateway)
    {
        return new LoginUseCase(loginGateway);
    }

    /// <summary>
    /// Performs the login of an employee using the provided data.
    /// </summary>
    /// <param name="id">Employee identifier.</param>
    /// <param name="name">Employee name.</param>
    /// <param name="role">Employee role.</param>
    /// <returns>
    /// Returns a string representing the login result (e.g., authentication token).
    /// </returns>
    public string Execute(int id, string name, EmployeeRole role)
    {
        return _loginGateway.Login(id, name, role);
    }
}

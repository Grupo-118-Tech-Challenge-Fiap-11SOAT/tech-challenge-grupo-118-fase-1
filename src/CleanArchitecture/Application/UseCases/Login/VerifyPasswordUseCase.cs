using Common.Interfaces.Login.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Login;

/// <summary>
/// Use case responsible for verifying the password provided by the user.
/// </summary>
public class VerifyPasswordUseCase
{
    private readonly ILoginGateway _loginGateway;

    /// <summary>
    /// Initializes a new instance of <see cref="VerifyPasswordUseCase"/>.
    /// </summary>
    /// <param name="loginGateway">Gateway responsible for login operations.</param>
    public VerifyPasswordUseCase(ILoginGateway loginGateway)
    {
        _loginGateway = loginGateway;
    }

    /// <summary>
    /// Creates an instance of <see cref="VerifyPasswordUseCase"/>.
    /// </summary>
    /// <param name="loginGateway">Gateway responsible for login operations.</param>
    /// <returns>A new instance of <see cref="VerifyPasswordUseCase"/>.</returns>
    public static VerifyPasswordUseCase Create(ILoginGateway loginGateway)
    {
        return new VerifyPasswordUseCase(loginGateway);
    }

    /// <summary>
    /// Executes the verification of the provided password against the stored password.
    /// </summary>
    /// <param name="password">Password provided by the user.</param>
    /// <param name="storedPassword">Stored password for comparison.</param>
    /// <returns><c>true</c> if the password is valid; otherwise, <c>false</c>.</returns>
    public bool Execute(string password, string storedPassword)
    {
        return _loginGateway.VerifyPassword(password, storedPassword);
    }
}

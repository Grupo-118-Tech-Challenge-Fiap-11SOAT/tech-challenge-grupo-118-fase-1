using Common.Enums;
using Common.Interfaces.Employee;
using Common.Interfaces.Login.Gateway;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Login;

/// <summary>
/// Login gateway implementation, responsible for authentication and password verification.
/// </summary>
public class LoginGateway : ILoginGateway
{
    /// <summary>
    /// Password manager used for authentication operations.
    /// </summary>
    private readonly IPasswordManager _passwordManager;

    /// <summary>
    /// Initializes a new instance of <see cref="LoginGateway"/>.
    /// </summary>
    /// <param name="passwordManager">Instance of <see cref="IPasswordManager"/> for password management.</param>
    public LoginGateway(IPasswordManager passwordManager)
    {
        _passwordManager = passwordManager;
    }

    /// <summary>
    /// Creates an instance of <see cref="ILoginGateway"/> using the provided password manager.
    /// </summary>
    /// <param name="passwordManager">Instance of <see cref="IPasswordManager"/>.</param>
    /// <returns>A new instance of <see cref="ILoginGateway"/>.</returns>
    public static ILoginGateway Create(IPasswordManager passwordManager)
    {
        return new LoginGateway(passwordManager);
    }

    /// <summary>
    /// Performs employee login, generating an authentication token.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <param name="name">Nome do funcionário.</param>
    /// <param name="role">Cargo do funcionário.</param>
    /// <returns>Token de autenticação gerado.</returns>
    public string Login(int id, string name, Roles role)
    {
        return _passwordManager.CreateToken(id, name, role);
    }

    /// <summary>
    /// Checks if the provided password matches the stored password.
    /// </summary>
    /// <param name="password">Plain text password provided by the user.</param>
    /// <param name="storedPassword">Stored (hashed) password.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    public bool VerifyPassword(string password, string storedPassword)
    {
        return _passwordManager.VerifyPassword(password, storedPassword);
    }
}

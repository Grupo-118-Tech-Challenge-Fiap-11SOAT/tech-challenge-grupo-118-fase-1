using Common.Interfaces.Login.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Login;

/// <summary>
/// Caso de uso responsável por verificar a senha informada pelo usuário.
/// </summary>
public class VerifyPasswordUseCase
{
    private readonly ILoginGateway _loginGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="VerifyPasswordUseCase"/>.
    /// </summary>
    /// <param name="loginGateway">Gateway responsável pelas operações de login.</param>
    public VerifyPasswordUseCase(ILoginGateway loginGateway)
    {
        _loginGateway = loginGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="VerifyPasswordUseCase"/>.
    /// </summary>
    /// <param name="loginGateway">Gateway responsável pelas operações de login.</param>
    /// <returns>Uma nova instância de <see cref="VerifyPasswordUseCase"/>.</returns>
    public static VerifyPasswordUseCase Create(ILoginGateway loginGateway)
    {
        return new VerifyPasswordUseCase(loginGateway);
    }

    /// <summary>
    /// Executa a verificação da senha informada em relação à senha armazenada.
    /// </summary>
    /// <param name="password">Senha informada pelo usuário.</param>
    /// <param name="storedPassword">Senha armazenada para comparação.</param>
    /// <returns><c>true</c> se a senha for válida; caso contrário, <c>false</c>.</returns>
    public bool Execute(string password, string storedPassword)
    {
        return _loginGateway.VerifyPassword(password, storedPassword);
    }
}

using Common.Enums;
using Common.Interfaces.Login.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Login;

/// <summary>
/// Caso de uso responsável por realizar o login de funcionários.
/// </summary>
public class LoginUseCase
{
    private readonly ILoginGateway _loginGateway;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="LoginUseCase"/>.
    /// </summary>
    /// <param name="loginGateway">Gateway responsável pela autenticação de login.</param>
    public LoginUseCase(ILoginGateway loginGateway)
    {
        _loginGateway = loginGateway;
    }

    /// <summary>
    /// Cria uma instância de <see cref="LoginUseCase"/> utilizando o gateway informado.
    /// </summary>
    /// <param name="loginGateway">Gateway responsável pela autenticação de login.</param>
    /// <returns>Instância de <see cref="LoginUseCase"/>.</returns>
    public static LoginUseCase Create(ILoginGateway loginGateway)
    {
        return new LoginUseCase(loginGateway);
    }

    /// <summary>
    /// Realiza o login de um funcionário utilizando os dados informados.
    /// </summary>
    /// <param name="id">Identificador do funcionário.</param>
    /// <param name="name">Nome do funcionário.</param>
    /// <param name="role">Cargo do funcionário.</param>
    /// <returns>
    /// Retorna uma string representando o resultado do login (ex: token de autenticação).
    /// </returns>
    public string Execute(int id, string name, Roles role)
    {
        return _loginGateway.Login(id, name, role);
    }
}

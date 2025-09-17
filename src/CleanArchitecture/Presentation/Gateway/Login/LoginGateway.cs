using Common.Enums;
using Common.Interfaces.Employee;
using Common.Interfaces.Login.Gateway;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Login;

/// <summary>
/// Implementação do gateway de login, responsável por autenticação e verificação de senha.
/// </summary>
public class LoginGateway : ILoginGateway
{
    /// <summary>
    /// Gerenciador de senhas utilizado para operações de autenticação.
    /// </summary>
    private readonly IPasswordManager _passwordManager;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="LoginGateway"/>.
    /// </summary>
    /// <param name="passwordManager">Instância de <see cref="IPasswordManager"/> para gerenciamento de senhas.</param>
    public LoginGateway(IPasswordManager passwordManager)
    {
        _passwordManager = passwordManager;
    }

    /// <summary>
    /// Cria uma instância de <see cref="ILoginGateway"/> utilizando o gerenciador de senhas fornecido.
    /// </summary>
    /// <param name="passwordManager">Instância de <see cref="IPasswordManager"/>.</param>
    /// <returns>Uma nova instância de <see cref="ILoginGateway"/>.</returns>
    public static ILoginGateway Create(IPasswordManager passwordManager)
    {
        return new LoginGateway(passwordManager);
    }

    /// <summary>
    /// Realiza o login do funcionário, gerando um token de autenticação.
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
    /// Verifica se a senha informada corresponde à senha armazenada.
    /// </summary>
    /// <param name="password">Senha em texto puro informada pelo usuário.</param>
    /// <param name="storedPassword">Senha armazenada (hash).</param>
    /// <returns>Verdadeiro se a senha corresponder; caso contrário, falso.</returns>
    public bool VerifyPassword(string password, string storedPassword)
    {
        return _passwordManager.VerifyPassword(password, storedPassword);
    }
}

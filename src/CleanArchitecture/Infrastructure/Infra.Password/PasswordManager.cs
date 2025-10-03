using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Dto.Employee;
using Common.Dto.Login;
using Common.Enums;
using Common.Interfaces.Employee;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Password;

/// <summary>
/// Gerencia operações relacionadas à senha e autenticação, incluindo criação de hash de senha,
/// verificação de senha e geração de tokens JWT para funcionários.
/// </summary>
public class PasswordManager : IPasswordManager
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="PasswordManager"/>.
    /// </summary>
    /// <param name="configuration">Configuração de aplicação para acessar chaves de segurança e parâmetros JWT.</param>
    public PasswordManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Create the password manager instance.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IPasswordManager Create(IConfiguration configuration)
    {
        return new PasswordManager(configuration);
    }
    
    /// <summary>
    /// Cria um hash para a senha especificada usando HMACSHA512 e uma chave secreta.
    /// </summary>
    /// <param name="password">A senha em texto plano a ser hasheada.</param>
    /// <param name="passwordHash">O hash resultante da senha como uma string codificada em Base64.</param>
    public void CreatePasswordHash(string password, out string passwordHash)
    {
        var secretKey = Encoding.UTF8.GetBytes(_configuration["Security:Key"]);
        using var hmac = new HMACSHA512(secretKey);
        passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    /// <summary>
    /// Cria um token JWT para o funcionário especificado.
    /// </summary>
    /// <param name="id">Identificador único do funcionário.</param>
    /// <param name="name">Nome do funcionário.</param>
    /// <param name="role">Cargo do funcionário.</param>
    /// <returns>Uma string representando o token JWT gerado.</returns>
    public string CreateToken(int id, string name, Roles role)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role.ToString())
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["Jwt:ExpirationMinutes"])
            ),
            SigningCredentials = creds,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Verifica se a senha fornecida corresponde ao hash armazenado.
    /// </summary>
    /// <param name="password">A senha em texto plano a ser verificada.</param>
    /// <param name="storedHash">O hash da senha para comparação.</param>
    /// <returns>Verdadeiro se a senha corresponder ao hash armazenado; caso contrário, falso.</returns>
    public bool VerifyPassword(string password, string storedHash)
    {
        var secretKey = Encoding.UTF8.GetBytes(_configuration["Security:Key"]);
        using var hmac = new HMACSHA512(secretKey);
        var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return computedHash == storedHash;
    }
}

using Domain.Employee.Dtos;
using Domain.Employee.Ports.Out;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infra.Password
{
    public class PasswordManager : IPasswordManager
    {
        private readonly IConfiguration _configuration;
        public PasswordManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>  
        /// Creates a hash for the specified password using HMACSHA512 and a secret key.  
        /// </summary>  
        /// <param name="password">The plain text password to hash.</param>  
        /// <param name="passwordHash">The resulting hashed password as a Base64-encoded string.</param>  
        public void CreatePasswordHash(string password, out string passwordHash)
        {
            var secretKey = Encoding.UTF8.GetBytes(_configuration["Security:Key"]);
            using var hmac = new HMACSHA512(secretKey);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        /// <summary>
        /// Creates a JWT token for the specified employee.
        /// </summary>
        /// <param name="employee">The employee for whom the token is being created.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public string CreateToken(EmployeeDto employee)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
               new Claim(ClaimTypes.Name, employee.Name),
               new Claim(ClaimTypes.Role, employee.Role.ToString())
           };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpirationMinutes"]
                    )),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Verifies whether the provided password matches the stored hash.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="storedHash">The hashed password to compare against.</param>
        /// <returns>True if the password matches the stored hash; otherwise, false.</returns>
        public bool VerifyPassword(string password, string storedHash)
        {
            var secretKey = Encoding.UTF8.GetBytes(_configuration["Security:Key"]);
            using var hmac = new HMACSHA512(secretKey);
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }
    }
}

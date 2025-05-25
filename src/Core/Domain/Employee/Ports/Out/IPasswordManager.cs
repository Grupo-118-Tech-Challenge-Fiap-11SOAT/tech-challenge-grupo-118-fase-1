using Domain.Employee.Dtos;

namespace Domain.Employee.Ports.Out;

public interface IPasswordManager
{
    /// <summary>
    /// Creates a token for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom the token is being created.</param>
    /// <returns>A string representing the generated token.</returns>
    string CreateToken(EmployeeDto employee);
    /// <summary>
    /// Verifies whether the provided password matches the stored hash.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="storedHash">The hashed password to compare against.</param>
    /// <returns>True if the password matches the stored hash; otherwise, false.</returns>
    bool VerifyPassword(string password, string storedHash);
    /// <summary>
    /// Creates a hash for the specified password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <param name="passwordHash">The resulting hashed password.</param>
    void CreatePasswordHash(string password, out string passwordHash);
}

using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Exceptions;

/// <summary>
/// Exception that is thrown when a password is null or empty.
/// </summary>
public class PasswordNullOrEmptyException : DomainException
{
    /// <summary>
    /// Gets the error message that explains the reason for the exception.
    /// </summary>
    public override string Message => "Password was null or empty.";
}
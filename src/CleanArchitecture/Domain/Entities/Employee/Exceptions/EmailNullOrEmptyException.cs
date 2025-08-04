using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Exceptions;

/// <summary>
/// Exception that is thrown when an email is null or empty.
/// </summary>
public class EmailNullOrEmptyException : DomainException
{
    /// <summary>
    /// Gets the error message that explains the reason for the exception.
    /// </summary>
    public override string Message => "Email was null or empty.";
}
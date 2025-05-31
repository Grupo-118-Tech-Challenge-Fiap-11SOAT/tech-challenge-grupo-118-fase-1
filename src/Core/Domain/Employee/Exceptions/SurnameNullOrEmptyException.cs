using Domain.Base.Exceptions;

namespace Domain.Employee.Exceptions;

/// <summary>
/// Exception that is thrown when the surname is null or empty.
/// </summary>
public class SurnameNullOrEmptyException : DomainException
{
    /// <summary>
    /// Gets the error message that explains the reason for the exception.
    /// </summary>
    public override string Message => "Surname was null or empty.";
}
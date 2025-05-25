namespace Domain.Employee.Exceptions;

/// <summary>
/// Exception thrown when the name is null or empty.
/// </summary>
public class NameNullOrEmptyException : DomainException
{
    /// <summary>
    /// Gets the error message indicating that the name was null or empty.
    /// </summary>
    public override string Message => "Name was null or empty.";
}
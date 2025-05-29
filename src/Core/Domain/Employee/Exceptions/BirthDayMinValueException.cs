namespace Domain.Employee.Exceptions;

/// <summary>
/// Exception thrown when a birth date is less than the minimum allowed value of January 1, 1900.
/// </summary>
public class BirthDayMinValueException : DomainException
{
    /// <summary>
    /// Gets the error message indicating that the birth date is invalid.
    /// </summary>
    public override string Message => "BirthDay was less than 1900-01-01.";
}
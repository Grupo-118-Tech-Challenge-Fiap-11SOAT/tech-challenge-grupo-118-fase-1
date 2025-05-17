namespace Domain.Employee.Exceptions;

public class BirthDayMinValueException : Exception
{
    public override string Message => "BirthDay was less than 1900-01-01.";
}

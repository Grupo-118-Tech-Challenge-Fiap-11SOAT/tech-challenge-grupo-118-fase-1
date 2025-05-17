namespace Domain.Employee.Exceptions;

public class NameNullOrEmptyException : Exception
{
    public override string Message => "Name was null or empty.";
}
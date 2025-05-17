namespace Domain.Employee.Exceptions;

public class EmailNullOrEmptyException : Exception
{
    public override string Message => "Email was null or empty.";
}
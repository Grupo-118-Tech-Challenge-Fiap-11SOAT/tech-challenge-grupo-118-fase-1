namespace Domain.Employee.Exceptions;

public class EmailNullOrEmptyException : Exception
{
    override public string Message => "Email was null or empty.";
}
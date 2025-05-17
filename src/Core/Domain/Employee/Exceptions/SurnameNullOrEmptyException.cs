namespace Domain.Employee.Exceptions;

public class SurnameNullOrEmptyException : Exception
{
    public override string Message => "Surname was null or empty.";
}
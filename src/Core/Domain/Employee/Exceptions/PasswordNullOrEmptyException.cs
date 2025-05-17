namespace Domain.Employee.Exceptions;
public class PasswordNullOrEmptyException : Exception
{
    public override string Message => "Password was null or empty.";
}

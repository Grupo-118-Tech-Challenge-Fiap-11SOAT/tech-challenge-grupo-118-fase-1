namespace Domain.Employee.Exceptions;
public class CpfNullOrEmptyException : Exception
{
    public override string Message => "CPF was null or empty.";
}

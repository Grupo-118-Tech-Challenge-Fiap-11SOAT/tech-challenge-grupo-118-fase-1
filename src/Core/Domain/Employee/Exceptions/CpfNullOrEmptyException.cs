namespace Domain.Employee.Exceptions;
public class CpfNullOrEmptyException : Exception
{
    public override string Message => "Cpf was null or empty.";
}

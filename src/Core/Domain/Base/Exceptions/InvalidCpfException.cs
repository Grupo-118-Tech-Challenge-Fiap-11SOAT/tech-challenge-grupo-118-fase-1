namespace Domain.Base.Exceptions
{
    public class InvalidCpfException : Exception
    {
        public override string Message => "CPF was invalid.";
    }
}

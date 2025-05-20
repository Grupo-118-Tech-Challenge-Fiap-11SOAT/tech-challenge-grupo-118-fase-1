namespace Domain.Base.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public override string Message => "Email was invalid.";
    }
}

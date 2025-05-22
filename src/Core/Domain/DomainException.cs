namespace Domain;

public class DomainException : Exception
{
    public DomainException()
    {
        
    }
    
    internal DomainException(string businessMessage)
        : base(businessMessage)
    {
    }
}
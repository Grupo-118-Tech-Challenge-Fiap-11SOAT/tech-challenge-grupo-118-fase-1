namespace Application.Products.Exceptions;

/// <summary>
/// Represents an exception that is thrown when one or more provided products are invalid or do not exist.
/// </summary>
public class InvalidProductException : ApplicationException
{
    /// <summary>
    /// Gets the message that describes the current exception.
    /// </summary>
    public override string Message => "Some informed products doesn't exist. Please check your data and try again.";
}
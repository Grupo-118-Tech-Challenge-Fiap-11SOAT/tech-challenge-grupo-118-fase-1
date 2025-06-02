namespace Application.Products.Exceptions;

/// <summary>
/// Thrown when attempting to access or manipulate a product that has been deactivated.
/// </summary>
public class DeactivatedProductException : ApplicationException
{
    /// <summary>
    /// Gets the error message indicating that the product has been deactivated and cannot be accessed or manipulated.
    /// </summary>
    public override string Message => "Deactivated product is not allowed.";
}
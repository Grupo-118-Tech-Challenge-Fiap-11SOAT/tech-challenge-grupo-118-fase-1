namespace Domain.Products.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an image position is considered invalid.
/// </summary>
/// <remarks>
/// This exception is typically used to enforce business rules related to the positioning
/// of images in the system. For example, the position should not be less than or equal to zero.
/// </remarks>
public class ImagePositionException : DomainException
{
    /// <summary>
    /// Gets the error message indicating that the image position is invalid.
    /// </summary>
    public override string Message => "Image position is invalid";
}
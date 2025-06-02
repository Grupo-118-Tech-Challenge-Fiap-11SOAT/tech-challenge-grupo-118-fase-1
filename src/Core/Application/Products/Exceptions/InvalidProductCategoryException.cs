using Domain.Products.ValueObjects;

namespace Application.Products.Exceptions;

/// <summary>
/// Exception that is thrown when an invalid product category is provided.
/// </summary>
/// <remarks>
/// This exception is used to handle situations where a product category provided
/// by the user does not match any of the predefined valid categories defined
/// in the <see cref="ProductType"/> enumeration.
/// </remarks>
public class InvalidProductCategoryException : ApplicationException
{
    private readonly string[] _validCategories;

    /// <summary>
    /// Exception that is thrown when an invalid product category is provided.
    /// </summary>
    /// <remarks>
    /// The exception is used to validate if the provided product category matches any of the allowed types
    /// in the <see cref="ProductType"/> enum. If the validation fails, this exception provides a detailed
    /// error message listing the valid product categories.
    /// </remarks>
    public InvalidProductCategoryException()
    {
        _validCategories = Enum.GetNames(typeof(ProductType));
    }

    /// <summary>
    /// Gets the error message that provides detailed information about the exception,
    /// including the valid product categories.
    /// </summary>
    /// <remarks>
    /// This property dynamically constructs the error message for the exception by
    /// listing all valid product categories retrieved from the <see cref="ProductType"/> enumeration.
    /// It is helpful for providing clarity to the user on what product categories are allowed.
    /// </remarks>
    public override string Message =>
        $"The informed product type is invalid. The allowed types are: {string.Join(", ", _validCategories)}";
}
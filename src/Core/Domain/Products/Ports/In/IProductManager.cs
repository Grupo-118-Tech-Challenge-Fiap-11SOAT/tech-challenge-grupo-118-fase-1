using Domain.Products.Dtos;

namespace Domain.Products.Ports.In;

public interface IProductManager
{
    /// <summary>
    /// Retrieves a list of products with pagination.
    /// </summary>
    /// <param name="skip">The number of products to skip from the beginning of the list.</param>
    /// <param name="take">The maximum number of products to include in the result.</param>
    /// <returns>A task representing an asynchronous operation that returns a list of ProductDto objects.</returns>
    Task<List<ProductDto>> GetProductsAsync(int skip = 0, int take = 10);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <returns>A task representing an asynchronous operation that returns a ProductDto object if found, or null if the product does not exist.</returns>
    Task<ProductDto> GetProductByIdAsync(int id);

    /// <summary>
    /// Creates a new product using the provided product details.
    /// </summary>
    /// <param name="productDto">The details of the product to create, encapsulated in a ProductDto object.</param>
    /// <returns>A task representing an asynchronous operation that returns the unique identifier of the created product.</returns>
    Task<int> CreateProductAsync(ProductDto productDto);

    /// <summary>
    /// Updates an existing product with the specified details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to update.</param>
    /// <param name="productDto">The data transfer object containing updated product details.</param>
    /// <returns>A task representing an asynchronous operation that returns the number of rows affected by the update.</returns>
    Task<int> UpdateProductAsync(int productId, ProductDto productDto);
}
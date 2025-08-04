using Common.Dto.Products;

namespace Common.Interfaces.Products.Controller;

public interface IProductController
{
    /// <summary>
    /// Retrieves a list of products with optional pagination and filtering for active products.
    /// </summary>
    /// <param name="skip">The number of products to skip for pagination.</param>
    /// <param name="take">The number of products to take for pagination.</param>
    /// <param name="searchActiveProducts">If true, only active products will be returned.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A list of <see cref="ProductDto"/> or null.</returns>
    Task<List<ProductDto>?> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products by type with optional pagination.
    /// </summary>
    /// <param name="type">The type of products to retrieve.</param>
    /// <param name="skip">The number of products to skip for pagination.</param>
    /// <param name="take">The number of products to take for pagination.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A list of <see cref="ProductDto"/> or null.</returns>
    Task<List<ProductDto>?> GetProductsByTypeAsync(string type, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its ID, with options to include images and pagination for related data.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ProductDto"/> or null.</returns>
    Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="productDto">The product data to create.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The created <see cref="ProductDto"/>.</returns>
    Task<ProductDto> CreateProductAsync(ProductDto productDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="productId">The ID of the product to update.</param>
    /// <param name="productDto">The updated product data.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The updated <see cref="ProductDto"/> or null if not found.</returns>
    Task<ProductDto?> UpdateProductAsync(int productId, ProductDto productDto,
        CancellationToken cancellationToken = default);
}